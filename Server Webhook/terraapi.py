import logging
import flask
from flask import request
from terra.base_client import Terra
import os
import dotenv
import json

dotenv.load_dotenv()

logging.basicConfig(level=logging.INFO)
_LOGGER = logging.getLogger("app")

API_KEY = os.getenv("API_KEY")
DEV_ID = os.getenv("DEV_ID")
SIGNING_SECRET = os.getenv("SIGNING_SECRET")

terra = Terra(api_key=API_KEY, dev_id=DEV_ID, secret=SIGNING_SECRET)

app = flask.Flask(__name__)

@app.route("/", methods=["GET"])
def home():
    return flask.Response(status=200, response="Hello World")

@app.route("/success", methods=["GET"])
def success():
    return flask.Response(status=200)

@app.route("/failure", methods=["GET"])
def failure():
    return flask.Response(status=400)

@app.route("/auth/generateAuthToken", methods=["POST"])
def gen_auth():
    auth_resp = terra.generate_authentication_url(
        reference_id="USER ID IN YOUR APP",
        resource="FITBIT",
        auth_success_redirect_url="https://success.url",
        auth_failure_redirect_url="https://failure.url",
    ).get_parsed_response()
    print(auth_resp)
    return flask.Response(status=200, response=auth_resp)


@app.route("/new_connection", methods=["GET"])
def new_connection():
    widget_response = terra.generate_widget_session(
        reference_id="michael.wiradharma",
	    providers=["APPLE"],
	    auth_success_redirect_url="http://localhost:8080/success",
        auth_failure_redirect_url="http://localhost:8080/failure",
        language="en"
    ).get_parsed_response()
    print(widget_response)
    return flask.Response(status=200, response=widget_response)

@app.route("/connections", methods=["GET"])
def get_connections():
    parsed_api_response = terra.list_users().get_parsed_response()
    print(parsed_api_response)
    return flask.Response(status=200)
    

@app.route("/consumeTerraWebhook", methods=["POST"])
def consume_terra_webhook() -> flask.Response:
    # body_str = str(request.get_data(), 'utf-8')
    body = request.get_json()
    if body["type"] == "body":
        with open("../Assets/Resources/data.json", "w") as f:
            data = body["data"][0]["heart_data"]["heart_rate_data"]["detailed"]["hr_samples"]
            data = {"heart_rate": data}
            json.dump(data, f, indent=2)
    _LOGGER.info(
        "Received webhook for user %s of type %s",
        body.get("user", {}).get("user_id"),
        body["type"])
    verified = terra.check_terra_signature(request.get_data().decode("utf-8"), request.headers['terra-signature'])
    if verified:
      return flask.Response(status=200)
    else:
      return flask.Response(status=403)
    
    
if __name__ == "__main__":
    app.run(host="localhost", port=8080, debug=True)