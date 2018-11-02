import json


SUCCESS_MESSAGE = {'status': 'success'}


def error_message(message):
    return json.dumps({
        'status': 'error',
        'message': message
    })
