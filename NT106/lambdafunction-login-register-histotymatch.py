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

# --- SNS (dùng để gửi mã qua email, nhớ sửa ARN thật) ---
sns = boto3.client('sns')
SNS_TOPIC_ARN = "arn:aws:sns:ap-southeast-1:123456789012:YOUR_TOPIC_NAME"

# --- JSON helper (Decimal -> int/float) ---
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

# ============================
# Email mã reset
# ============================
def send_reset_code_email(email, username, code):
    subject = "Mã đặt lại mật khẩu - Plane Fighting Super Start"
    message = (
        f"Xin chào {username},\n\n"
        f"Mã đặt lại mật khẩu của bạn là: {code}\n"
        f"Mã này có hiệu lực trong 10 phút.\n\n"
        "Nếu bạn không yêu cầu đặt lại mật khẩu, hãy bỏ qua email này."
    )
    sns.publish(TopicArn=SNS_TOPIC_ARN, Subject=subject, Message=message)

# ============================
# 1) Đăng ký
# ============================
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
                "UpgradeDamage": 0,

                # ✅ Khởi tạo flag nhận quà
                "RewardLv10Claimed": False,
                "RewardLv50Claimed": False,
                "RewardLv100Claimed": False
            },
            ConditionExpression="attribute_not_exists(Username)"
        )
        return create_response(200, {"message": "Đăng ký thành công"})
    except ClientError as e:
        if e.response['Error']['Code'] == 'ConditionalCheckFailedException':
            return create_response(409, {"message": "Tên đăng nhập đã tồn tại"})
        print("Lỗi ghi DynamoDB:", e)
        return create_response(500, {"message": "Lỗi hệ thống khi đăng ký"})

# ============================
# 2) Đăng nhập
# ============================
def handle_login(body):
    username = body.get("Username", "").strip()
    password = body.get("Password", "").strip()
    if not username or not password:
        return create_response(400, {"message": "Thiếu Username hoặc Password"})

    resp = ACCOUNT_TABLE.get_item(Key={'Username': username})
    acc = resp.get('Item')
    if not acc:
        return create_response(401, {"message": "Username không tồn tại"})
    if str(acc.get("Password", "")) != password:
        return create_response(401, {"message": "Password không đúng"})

    # Không trả password về client
    acc.pop("Password", None)

    # ✅ đảm bảo luôn có 3 flag nếu chưa từng được set
    acc.setdefault("RewardLv10Claimed", False)
    acc.setdefault("RewardLv50Claimed", False)
    acc.setdefault("RewardLv100Claimed", False)

    return create_response(200, acc)

# ============================
# 3) Cập nhật account
# ============================
def handle_update_account(body):
    username = body.get("Username", "").strip()
    if not username:
        return create_response(400, {"message": "Thiếu Username"})

    # Lấy các giá trị từ body
    gold = int(body.get("Gold", 0))
    upgrade_hp = int(body.get("UpgradeHP", 100))
    upgrade_damage = int(body.get("UpgradeDamage", 0))
    level = int(body.get("Level", 1))

    # ✅ Flag nhận thưởng
    reward10 = bool(body.get("RewardLv10Claimed", False))
    reward50 = bool(body.get("RewardLv50Claimed", False))
    reward100 = bool(body.get("RewardLv100Claimed", False))

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression=(
                "SET Gold = :g, "
                "UpgradeHP = :h, "
                "UpgradeDamage = :d, "
                "#L = :l, "
                "RewardLv10Claimed = :r10, "
                "RewardLv50Claimed = :r50, "
                "RewardLv100Claimed = :r100"
            ),
            ExpressionAttributeValues={
                ":g": gold,
                ":h": upgrade_hp,
                ":d": upgrade_damage,
                ":l": level,
                ":r10": reward10,
                ":r50": reward50,
                ":r100": reward100,
            },
            ExpressionAttributeNames={"#L": "Level"},
            ConditionExpression="attribute_exists(Username)"
        )
        return create_response(200, {"message": "Cập nhật thành công"})
    except ClientError as e:
        if e.response['Error']['Code'] == 'ConditionalCheckFailedException':
            return create_response(404, {"message": "Không tìm thấy tài khoản để cập nhật."})
        print("Lỗi cập nhật:", e)
        return create_response(500, {"message": "Lỗi hệ thống khi cập nhật"})

# ============================
# 4) Ghi lịch sử đấu
# ============================
def handle_record_match(body):
    winner = body.get("WinnerUsername", "").strip()
    loser  = body.get("LoserUsername", "").strip()
    if not winner or not loser:
        return create_response(400, {"message": "Thiếu Winner/Loser"})

    match_id   = body.get("Id") or f"match-{uuid.uuid4().hex}"
    match_date = body.get("MatchDate") or datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%SZ")

    item = {
        "Id": match_id,
        "WinnerUsername": winner,
        "LoserUsername":  loser,
        "MatchDate": match_date
    }

    try:
        MATCH_TABLE.put_item(Item=item)
        return create_response(200, {"message": "Lưu lịch sử thành công", "item": item})
    except ClientError as e:
        print("Lỗi ghi DynamoDB:", e)
        return create_response(500, {"message": "Lỗi hệ thống khi ghi lịch sử"})

# ============================
# 5) Lấy lịch sử đấu
# ============================
def handle_get_match_history(username):
    username = (username or "").strip()
    if not username:
        return create_response(400, {"message": "Thiếu username"})

    try:
        collected = []

        # Query theo GSI WinnerUsername (nếu có)
        try:
            resp_w = MATCH_TABLE.query(
                IndexName="WinnerUsername-index",
                KeyConditionExpression=Key("WinnerUsername").eq(username)
            )
            collected.extend(resp_w.get("Items", []))
        except ClientError as e:
            print("GSI WinnerUsername-index query failed:",
                  e.response.get("Error", {}).get("Code"))

        # Query theo GSI LoserUsername (nếu có)
        try:
            resp_l = MATCH_TABLE.query(
                IndexName="LoserUsername-index",
                KeyConditionExpression=Key("LoserUsername").eq(username)
            )
            collected.extend(resp_l.get("Items", []))
        except ClientError as e:
            print("GSI LoserUsername-index query failed:",
                  e.response.get("Error", {}).get("Code"))

        # Fallback scan nếu chưa có GSI hoặc không có kết quả
        if not collected:
            resp_scan = MATCH_TABLE.scan(
                FilterExpression=Attr("WinnerUsername").eq(username) |
                                 Attr("LoserUsername").eq(username)
            )
            collected.extend(resp_scan.get("Items", []))

        # Loại trùng theo Id
        uniq = {}
        for it in collected:
            uniq[it.get("Id")] = it
        items = list(uniq.values())

        # Sort theo MatchDate giảm dần
        items.sort(key=lambda x: x.get("MatchDate", ""), reverse=True)

        return create_response(200, items)
    except Exception as e:
        print("Lỗi lấy lịch sử:", e)
        return create_response(500, {"message": "Lỗi hệ thống khi lấy lịch sử"})

# ============================
# 6) Quên mật khẩu: Gửi mã
# ============================
def handle_request_reset(body):
    username = body.get("Username", "").strip()
    email = body.get("Email", "").strip()
    if not username or not email:
        return create_response(400, {"message": "Thiếu Username hoặc Email"})

    resp = ACCOUNT_TABLE.get_item(Key={"Username": username})
    acc = resp.get("Item")
    if not acc:
        return create_response(404, {"message": "Tài khoản không tồn tại"})

    acc_email = str(acc.get("Email", "")).strip().lower()
    if acc_email != email.lower():
        return create_response(400, {"message": "Email không khớp với tài khoản"})

    code = f"{random.randint(100000, 999999)}"
    expiry = int(time.time()) + 10 * 60

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET ResetCode=:c, ResetCodeExpiry=:e",
            ExpressionAttributeValues={":c": code, ":e": expiry}
        )
        send_reset_code_email(email, username, code)
        return create_response(200, {"message": "Đã gửi mã đặt lại mật khẩu"})
    except Exception as e:
        print("Lỗi khi tạo/gửi mã reset:", e)
        return create_response(500, {"message": "Lỗi hệ thống khi gửi mã đặt lại mật khẩu"})

# ============================
# 7) Quên mật khẩu: Xác nhận + đổi
# ============================
def handle_confirm_reset(body):
    username = body.get("Username", "").strip()
    email = body.get("Email", "").strip()
    code = body.get("Code", "").strip()
    new_password = body.get("NewPassword", "").strip()

    if not username or not email or not code or not new_password:
        return create_response(400, {"message": "Thiếu thông tin (Username/Email/Code/NewPassword)"})

    resp = ACCOUNT_TABLE.get_item(Key={"Username": username})
    acc = resp.get("Item")
    if not acc:
        return create_response(404, {"message": "Tài khoản không tồn tại"})

    acc_email = str(acc.get("Email", "")).strip().lower()
    if acc_email != email.lower():
        return create_response(400, {"message": "Email không khớp với tài khoản"})

    stored_code = str(acc.get("ResetCode", ""))
    expiry = acc.get("ResetCodeExpiry", None)
    now = int(time.time())

    if not stored_code or stored_code != code:
        return create_response(400, {"message": "Mã xác minh không đúng"})
    if expiry is None or now > int(expiry):
        return create_response(400, {"message": "Mã xác minh đã hết hạn"})

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET Password=:p REMOVE ResetCode, ResetCodeExpiry",
            ExpressionAttributeValues={":p": new_password}
        )
        return create_response(200, {"message": "Đổi mật khẩu thành công"})
    except Exception as e:
        print("Lỗi khi đổi mật khẩu (confirm-reset):", e)
        return create_response(500, {"message": "Lỗi hệ thống khi đổi mật khẩu"})

# ============================
# 8) Đổi mật khẩu trực tiếp
# ============================
def handle_change_password(body):
    username = body.get("Username", "").strip()
    new_password = body.get("NewPassword", "").strip()
    if not username or not new_password:
        return create_response(400, {"message": "Thiếu Username hoặc NewPassword"})

    resp = ACCOUNT_TABLE.get_item(Key={"Username": username})
    acc = resp.get("Item")
    if not acc:
        return create_response(404, {"message": "Tài khoản không tồn tại"})

    try:
        ACCOUNT_TABLE.update_item(
            Key={"Username": username},
            UpdateExpression="SET Password=:p",
            ExpressionAttributeValues={":p": new_password}
        )
        return create_response(200, {"message": "Đổi mật khẩu thành công"})
    except Exception as e:
        print("Lỗi khi đổi mật khẩu (change-password):", e)
        return create_response(500, {"message": "Lỗi hệ thống khi đổi mật khẩu"})

# ============================
# Entry point (routing)
# ============================
def lambda_handler(event, context):
    http = (event.get("requestContext") or {}).get("http", {}) or {}
    http_method = http.get("method", "")
    raw_path = http.get("path", "") or ""

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
        if "/account/login" in raw_path:
            return handle_login(body)
        if "/matchhistory/add" in raw_path:
            return handle_record_match(body)
        if "/account/request-reset" in raw_path:
            return handle_request_reset(body)
        if "/account/confirm-reset" in raw_path:
            return handle_confirm_reset(body)
        if "/account/change-password" in raw_path:
            return handle_change_password(body)

    # --- PUT ---
    if http_method == "PUT":
        if "/account/update" in raw_path:
            return handle_update_account(body)

    # --- GET ---
    if http_method == "GET":
        params = event.get("pathParameters") or {}
        raw_username = params.get("username")

        # Phòng trường hợp HTTP API không map pathParameters
        if "/matchhistory/" in raw_path and not raw_username:
            raw_username = raw_path.split("/matchhistory/", 1)[-1].strip("/")

        # Lấy account
        if "/account/" in raw_path and raw_username:
            resp = ACCOUNT_TABLE.get_item(Key={'Username': raw_username})
            acc = resp.get('Item')
            if acc:
                acc.pop("Password", None)
                # ✅ default flag nếu chưa có
                acc.setdefault("RewardLv10Claimed", False)
                acc.setdefault("RewardLv50Claimed", False)
                acc.setdefault("RewardLv100Claimed", False)
                return create_response(200, acc)
            return create_response(404, {"message": "Tài khoản không tồn tại"})

        # Lịch sử đấu
        if "/matchhistory/" in raw_path and raw_username:
            return handle_get_match_history(raw_username)

    return create_response(404, {"message": f"Endpoint not found: {raw_path}"})
