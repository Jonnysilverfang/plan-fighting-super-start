import json
import boto3
import uuid
from datetime import datetime
from boto3.dynamodb.conditions import Key, Attr
from botocore.exceptions import ClientError
from decimal import Decimal
import random
import time

# --- DynamoDB ---
dynamodb = boto3.resource('dynamodb')
ACCOUNT_TABLE = dynamodb.Table('AccountData')
MATCH_TABLE = dynamodb.Table('MatchHistory')

# --- SNS (dùng để gửi mã qua email, bạn chỉnh ARN cho đúng) ---
sns = boto3.client('sns')
SNS_TOPIC_ARN = "arn:aws:sns:ap-southeast-1:123456789012:YOUR_TOPIC_NAME"  # TODO: sửa ARN thật

# --- Hỗ trợ JSON (Decimal -> int/float) ---
class DecimalEncoder(json.JSONEncoder):
    def default(self, obj):
        if isinstance(obj, Decimal):
            return int(obj) if obj % 1 == 0 else float(obj)
        return super().default(obj)

def create_response(statusCode, body):
    return {
        "statusCode": statusCode,
        "headers": {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
        "body": json.dumps(body, cls=DecimalEncoder)
    }

# --- Gửi email mã reset ---
def send_reset_code_email(email, username, code):
    subject = "Mã đặt lại mật khẩu - Plane Fighting Super Start"
    message = (
        f"Xin chào {username},\n\n"
        f"Mã đặt lại mật khẩu của bạn là: {code}\n"
        f"Mã này có hiệu lực trong 10 phút.\n\n"
        "Nếu bạn không yêu cầu đặt lại mật khẩu, hãy bỏ qua email này."
    )

    sns.publish(
        TopicArn=SNS_TOPIC_ARN,
        Subject=subject,
        Message=message
    )

# ==================================
# 1️⃣ Đăng ký
# ==================================
def handle_register(body):
    username = body.get("Username", "").strip()
    password = body.get("Password", "").strip()
    email = body.get("Email", "").strip()

    if not username or not password or not email:
        return create_response(400, {"message": "Thiếu Username, Password hoặc Email"})

    if "@" not in email or "." not in email:
        return create_response(400, {"message": "Email không hợp lệ"})

    try:
        ACCOUNT_TABLE.put_item(
            Item={
                "Username": username,
                "Password": password,
                "Email": email,
                "Gold": 0,
                "Level": 1,
                "UpgradeHP": 100,
                "UpgradeDamage": 0
            },
            ConditionExpression="attribute_not_exists(Username)"
        )
        return create_response(200, {"message": "Đăng ký thành công"})
    except ClientError as e:
        if e.response['Error']['Code'] == 'ConditionalCheckFailedException':
            return create_response(409, {"message": "Tên đăng nhập đã tồn tại"})
        print(f"Lỗi ghi DynamoDB: {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi đăng ký"})

# ==================================
# 2️⃣ Đăng nhập
# ==================================
def handle_login(body):
    username = body.get("Username", "").strip()
    password = body.get("Password", "").strip()

    if not username or not password:
        return create_response(400, {"message": "Thiếu Username hoặc Password"})

    response = ACCOUNT_TABLE.get_item(Key={'Username': username})
    account = response.get('Item')

    if not account:
        return create_response(401, {"message": "Username không tồn tại"})

    if str(account.get("Password", "")) != password:
        return create_response(401, {"message": "Password không đúng"})

    if "Password" in account:
        del account["Password"]

    return create_response(200, account)

# ==================================
# 3️⃣ Cập nhật account
# ==================================
def handle_update_account(body):
    username = body.get("Username", "").strip()
    if not username:
        return create_response(400, {"message": "Thiếu Username"})

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET Gold=:g, UpgradeHP=:h, UpgradeDamage=:d, #L=:l",
            ExpressionAttributeValues={
                ":g": int(body.get("Gold", 0)),
                ":h": int(body.get("UpgradeHP", 100)),
                ":d": int(body.get("UpgradeDamage", 0)),
                ":l": int(body.get("Level", 1))
            },
            ExpressionAttributeNames={"#L": "Level"},
            ConditionExpression="attribute_exists(Username)"
        )
        return create_response(200, {"message": "Cập nhật thành công"})
    except ClientError as e:
        if e.response['Error']['Code'] == 'ConditionalCheckFailedException':
            return create_response(404, {"message": "Không tìm thấy tài khoản để cập nhật."})
        print(f"Lỗi cập nhật: {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi cập nhật"})

# ==================================
# 4️⃣ Ghi lịch sử đấu
# ==================================
def handle_record_match(body):
    winner = body.get("WinnerUsername", "").strip()
    loser = body.get("LoserUsername", "").strip()
    match_id = body.get("Id", str(uuid.uuid4()))
    match_date = body.get("MatchDate", datetime.utcnow().isoformat())

    if not winner or not loser:
        return create_response(400, {"message": "Thiếu Winner/Loser"})

    item = {
        "Id": match_id,
        "WinnerUsername": winner,
        "LoserUsername": loser,
        "MatchDate": match_date
    }

    try:
        MATCH_TABLE.put_item(Item=item)
        return create_response(200, {"message": "Lưu lịch sử thành công", "item": item})
    except ClientError as e:
        print(f"Lỗi ghi DynamoDB: {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi ghi lịch sử"})

# ==================================
# 5️⃣ Lấy lịch sử đấu
# ==================================
def handle_get_match_history(username):
    username = username.strip()
    try:
        all_matches = []

        try:
            resp_wins = MATCH_TABLE.query(
                IndexName='WinnerUsername-index',
                KeyConditionExpression=Key('WinnerUsername').eq(username)
            )
            all_matches.extend(resp_wins.get("Items", []))
        except ClientError:
            pass

        try:
            resp_losses = MATCH_TABLE.query(
                IndexName='LoserUsername-index',
                KeyConditionExpression=Key('LoserUsername').eq(username)
            )
            all_matches.extend(resp_losses.get("Items", []))
        except ClientError:
            resp_scan = MATCH_TABLE.scan(
                FilterExpression=Attr("WinnerUsername").eq(username) | Attr("LoserUsername").eq(username)
            )
            all_matches.extend(resp_scan.get("Items", []))

        all_matches.sort(key=lambda x: x.get("MatchDate", ""), reverse=True)
        return create_response(200, all_matches)
    except Exception as e:
        print(f"Lỗi lấy lịch sử: {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi lấy lịch sử"})

# ==================================
# 6️⃣ QUÊN MẬT KHẨU: Gửi mã reset
# ==================================
def handle_request_reset(body):
    username = body.get("Username", "").strip()
    email = body.get("Email", "").strip()

    if not username or not email:
        return create_response(400, {"message": "Thiếu Username hoặc Email"})

    resp = ACCOUNT_TABLE.get_item(Key={"Username": username})
    account = resp.get("Item")

    if not account:
        return create_response(404, {"message": "Tài khoản không tồn tại"})

    acc_email = str(account.get("Email", "")).strip().lower()
    if acc_email != email.lower():
        return create_response(400, {"message": "Email không khớp với tài khoản"})

    code = f"{random.randint(100000, 999999)}"
    expiry = int(time.time()) + 10 * 60

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET ResetCode=:c, ResetCodeExpiry=:e",
            ExpressionAttributeValues={
                ":c": code,
                ":e": expiry
            }
        )

        send_reset_code_email(email, username, code)

        return create_response(200, {"message": "Đã gửi mã đặt lại mật khẩu"})
    except Exception as e:
        print(f"Lỗi khi tạo/gửi mã reset: {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi gửi mã đặt lại mật khẩu"})

# ==================================
# 7️⃣ QUÊN MẬT KHẨU: Xác nhận mã + đổi mật khẩu
# ==================================
def handle_confirm_reset(body):
    username = body.get("Username", "").strip()
    email = body.get("Email", "").strip()
    code = body.get("Code", "").strip()
    new_password = body.get("NewPassword", "").strip()

    if not username or not email or not code or not new_password:
        return create_response(400, {"message": "Thiếu thông tin (Username/Email/Code/NewPassword)"})

    resp = ACCOUNT_TABLE.get_item(Key={"Username": username})
    account = resp.get("Item")
    if not account:
        return create_response(404, {"message": "Tài khoản không tồn tại"})

    acc_email = str(account.get("Email", "")).strip().lower()
    if acc_email != email.lower():
        return create_response(400, {"message": "Email không khớp với tài khoản"})

    stored_code = str(account.get("ResetCode", ""))
    expiry = account.get("ResetCodeExpiry", None)
    now = int(time.time())

    if not stored_code or stored_code != code:
        return create_response(400, {"message": "Mã xác minh không đúng"})

    if expiry is None or now > int(expiry):
        return create_response(400, {"message": "Mã xác minh đã hết hạn"})

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET Password=:p REMOVE ResetCode, ResetCodeExpiry",
            ExpressionAttributeValues={
                ":p": new_password
            }
        )
        return create_response(200, {"message": "Đổi mật khẩu thành công"})
    except Exception as e:
        print(f"Lỗi khi đổi mật khẩu (confirm-reset): {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi đổi mật khẩu"})

# ==================================
# 8️⃣ ĐỔI MẬT KHẨU TRỰC TIẾP (ĐANG ĐĂNG NHẬP)
# ==================================
def handle_change_password(body):
    username = body.get("Username", "").strip()
    new_password = body.get("NewPassword", "").strip()

    if not username or not new_password:
        return create_response(400, {"message": "Thiếu Username hoặc NewPassword"})

    resp = ACCOUNT_TABLE.get_item(Key={"Username": username})
    account = resp.get("Item")

    if not account:
        return create_response(404, {"message": "Tài khoản không tồn tại"})

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET Password=:p",
            ExpressionAttributeValues={":p": new_password}
        )
        return create_response(200, {"message": "Đổi mật khẩu thành công"})
    except Exception as e:
        print(f"Lỗi khi đổi mật khẩu (change-password): {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi đổi mật khẩu"})

# ==================================
# Entry Point Lambda (routing mềm hơn)
# ==================================
def lambda_handler(event, context):
    http = event.get("requestContext", {}).get("http", {})
    http_method = http.get("method", "")
    raw_path = http.get("path", "")

    print("METHOD:", http_method)
    print("PATH:", raw_path)

    body = {}
    if http_method in ["POST", "PUT"] and event.get("body"):
        try:
            body = json.loads(event["body"])
        except json.JSONDecodeError:
            return create_response(400, {"message": "Invalid JSON body"})

    # --- POST ---
    if http_method == "POST":
        if "/account/register" in raw_path:
            return handle_register(body)
        elif "/account/login" in raw_path:
            return handle_login(body)
        elif "/matchhistory/add" in raw_path:
            return handle_record_match(body)
        elif "/account/request-reset" in raw_path:
            return handle_request_reset(body)
        elif "/account/confirm-reset" in raw_path:
            return handle_confirm_reset(body)
        elif "/account/change-password" in raw_path:
            return handle_change_password(body)

    # --- PUT ---
    elif http_method == "PUT":
        if "/account/update" in raw_path:
            return handle_update_account(body)

    # --- GET ---
    elif http_method == "GET":
        # với HTTP API, pathParameters có thể None, nên dùng dict rỗng cho chắc
        params = event.get("pathParameters") or {}
        username = params.get("username")

        if "/account/" in raw_path and username:
            response = ACCOUNT_TABLE.get_item(Key={'Username': username})
            account = response.get('Item')
            if account:
                if "Password" in account:
                    del account["Password"]
                return create_response(200, account)
            else:
                return create_response(404, {"message": "Tài khoản không tồn tại"})

        elif "/matchhistory/" in raw_path and username:
            return handle_get_match_history(username)

    return create_response(404, {"message": f"Endpoint not found: {raw_path}"})
