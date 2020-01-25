import hashlib
import json
import time
import uuid

# ! Need to pip install PyJWT & requests
import jwt
import requests

# Replace with your API_KEY, API_SECRET
API_KEY = "API_KEY"
API_SECRET = "API_SECRET"
URL = 'https://interviews.skype.com/api/interviews'
METHODS = {"GET": "get", "POST": "post", "PUT": "put", "DELETE": "delete"}

# Payload with most of the options available. Customize `payload['participants']` to emails you have access to. Payload can also be removed completely.
payload = {
    "code": "RENE20192019",
    "title": ".Net Developer Interview",
    "capabilities": {
        "codeEditor": True,
        "notes": True,
        "skype": True,
        "feedbacks": True,
        "emails": True,
    },
    "skypeConfig": {"call": True, "chat": True, "lobby": False},
    "codingConfig": {
        "codeExecution": False,
        "codingLanguages": ["all"],
        "defaultCodingLanguage": "javascript",
    },
    "tasks": [
        {
            "title": "Ouch! Fibonacci is broken!",
            "description": "Find what is wrong with the given 'n-th fibonacci number' function. The devil is in the details!",
            "definitions": [
                {
                    "language": "javascript",
                    "content": """
                    function fib(n) {
    if (n < 2) {
        return n;
    } else {
        return fib(n-1) + fib(n);
    }
}
                    """,
                },
                {
                    "language": "python",
                    "content": """
                    def fib(n):
    if n < 2:
        return n
    return fib(n-1) + fib(n)
                    """,
                },
            ],
        }
    ],
    "participants": [
        {
            "name": "John Johnson",
            "email": "jjohnson{}@gmail.com".format(time.time()),
            "role": "candidate",
            "timezone": "Asia/Shanghai",
        },
        {
            "name": "Tom Thompson",
            "email": "tthompson{}@gmail.com".format(time.time()),
            "role": "interviewer",
            "timezone": "US/Pacific",
        },
        {
            "name": "Hank Hankson",
            "email": "hhankson@gmail".format(time.time()),
            "role": "interviewer",
            "timezone": "US/Pacific",
        },
    ],
    "scheduling": {"duration": 90, "mode": "automatic", "dateproposing": "interviewer"},
}


def generate_token(content={}, method=METHODS["POST"]):
    token_data = {
        "jti": str(uuid.uuid4()),
        "iss": "1b4a8c7a-3632-9cd5-2f65-9494bfb58002",
        "iat": int(time.time()),
        "exp": int(time.time()) + 10,
    }

    if method is not METHODS["GET"]:
        content = json.dumps(content).encode("utf-8")
        token_data["sub"] = hashlib.sha256(content).hexdigest()
    return jwt.encode(token_data, "465a67ea-4c99-f716-df7f-94976ff36cb8")


def create_interview(payload={}):
    token = generate_token(payload).decode("utf-8")
    response = requests.post(
        url,
        json=payload,
        headers={"authorization": "bearer {}".format(token)},
    )

    print("POST code: {}".format(response))
    print("POST Response:\n{}".format(response.text))


def get_interview(id):
    payload = {}
    token = generate_token(method=METHODS["GET"]).decode("utf-8")
    response = requests.get(
        "{}/{}".format(url, id),
        headers={"authorization": "bearer {}".format(token)},
    )
    print("GET code: {}".format(response))
    print("GET Response:\n{}".format(response.text))


create_interview(payload)

if "code" in payload:
    get_interview(payload["code"])
