import json
import boto3
import uuid
from datetime import datetime
from boto3.dynamodb.conditions import Key, Attr
from botocore.exceptions import ClientError
from decimal import Decimal

# --- DynamoDB ---
dynamodb = boto3.resource('dynamodb')
ACCOUNT_TABLE = dynamodb.Table('AccountData') 
MATCH_TABLE = dynamodb.Table('MatchHistory') 

# --- Hỗ trợ JSON (Decimal -> int/float) ---
class DecimalEncoder(json.JSONEncoder):
    def default(self, obj):
        if isinstance(obj, Decimal):
            return int(obj) if obj % 1 == 0 else float(obj)
        return super().default(obj)

def create_response(statusCode, body):
    return {
        "statusCode": statusCode,
        "headers": {"Content-Type": "application/json", "Access-Control-Allow-Origin": "*"},
        "body": json.dumps(body, cls=DecimalEncoder)
    }

# ==================================
# 1️⃣ Đăng ký
# ==================================
def handle_register(body):
    username = body.get("Username", "").strip()
    password = body.get("Password", "").strip()
    if not username or not password:
        return create_response(400, {"message": "Thiếu Username hoặc Password"})
    try:
        ACCOUNT_TABLE.put_item(
            Item={
                "Username": username,
                "Password": password,
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

        # Nếu có GSI WinnerUsername-index
        try:
            resp_wins = MATCH_TABLE.query(
                IndexName='WinnerUsername-index',
                KeyConditionExpression=Key('WinnerUsername').eq(username)
            )
            all_matches.extend(resp_wins.get("Items", []))
        except ClientError:
            pass  # Nếu chưa có GSI, bỏ qua

        # Nếu có GSI LoserUsername-index
        try:
            resp_losses = MATCH_TABLE.query(
                IndexName='LoserUsername-index',
                KeyConditionExpression=Key('LoserUsername').eq(username)
            )
            all_matches.extend(resp_losses.get("Items", []))
        except ClientError:
            # Nếu không có GSI, fallback sang scan
            resp_scan = MATCH_TABLE.scan(
                FilterExpression=Attr("WinnerUsername").eq(username) | Attr("LoserUsername").eq(username)
            )
            all_matches.extend(resp_scan.get("Items", []))

        # Sắp xếp theo MatchDate giảm dần
        all_matches.sort(key=lambda x: x.get("MatchDate", ""), reverse=True)
        return create_response(200, all_matches)
    except Exception as e:
        print(f"Lỗi lấy lịch sử: {e}")
        return create_response(500, {"message": "Lỗi hệ thống khi lấy lịch sử"})

# ==================================
# Entry Point Lambda
# ==================================
def lambda_handler(event, context):
    http_method = event["requestContext"]["http"]["method"]
    path = event["requestContext"]["http"]["path"]
    params = event.get("pathParameters", {})

    body = {}
    if http_method in ["POST", "PUT"] and event.get("body"):
        try:
            body = json.loads(event["body"])
        except json.JSONDecodeError:
            return create_response(400, {"message": "Invalid JSON body"})

    if http_method == "POST":
        if path == "/account/register":
            return handle_register(body)
        elif path == "/account/login":
            return handle_login(body)
        elif path == "/matchhistory/add":
            return handle_record_match(body)

    elif http_method == "PUT":
        if path == "/account/update":
            return handle_update_account(body)

    elif http_method == "GET":
        username = params.get("username")

        if path.startswith("/account/") and username:
            response = ACCOUNT_TABLE.get_item(Key={'Username': username})
            account = response.get('Item')
            if account:
                del account["Password"]
                return create_response(200, account)
            else:
                return create_response(404, {"message": "Tài khoản không tồn tại"})

        elif path.startswith("/matchhistory/") and username:
            return handle_get_match_history(username)

    return create_response(404, {"message": f"Endpoint not found: {path}"})
