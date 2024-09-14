import logging
import flask
from flask import request
from terra.base_client import Terra

logging.basicConfig(level=logging.INFO)
_LOGGER = logging.getLogger("app")

terra = Terra(api_key='4iU5tHkyIxEMJARzVOagtGueOP41KMJl', dev_id='4actk-surroundmeditation-testing-1p6QwoJu88', secret="SIGNING-SECRET")

app = flask.Flask(__name__)

@app.route("/success", methods=["GET"])
def success():
    return flask.Response(status=200)

@app.route("/failure", methods=["GET"])
def failure():
    return flask.Response(status=400)

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
    

# @app.route("/consumeTerraWebhook", methods=["POST"])
# def consume_terra_webhook() -> flask.Response:
#     # body_str = str(request.get_data(), 'utf-8')
#     body = request.get_json()
#     _LOGGER.info(
#         "Received webhook for user %s of type %s",
#         body.get("user", {}).get("user_id"),
#         body["type"])
#     verified = terra.check_terra_signature(request.get_data().decode("utf-8"), request.headers['terra-signature'])
#     if verified:
#       return flask.Response(status=200)
#     else:
#       return flask.Response(status=403)
    
    
if __name__ == "__main__":
    app.run(host="localhost", port=8080, debug=True)