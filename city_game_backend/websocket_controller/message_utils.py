import json
import math

SUCCESS_MESSAGE = {'status': 'success'}


def error_message(message):
    return json.dumps({
        'status': 'error',
        'message': message
    })


# Rounds down to chunk coordinates (see CONSTANTS.CHUNK_SIZE)
def round_down(n):
    return math.floor(n * 100) / 100
