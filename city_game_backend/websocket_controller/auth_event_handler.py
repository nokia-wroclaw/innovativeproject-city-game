from django.contrib.auth import authenticate
import logging
from .active_connections_storage import ActiveConnectionsStorage
import json

logger = logging.getLogger(__name__)


def handle_auth_event(message: dict, websocket):
    """
    In order to authenticate, the user must send the following:
    login: username
    pass: user's password

    :param message:
    :param websocket:
    :return:
    """
    username = message['login']
    password = message['pass']

    logging.info(f'received auth request from {username}')
    user = authenticate(username=username, password=password)

    if user is not None:
        logging.info(f'User {username} authenticated!')
        websocket.user = user
        ActiveConnectionsStorage.add(user.id, websocket)
        websocket.send(
            json.dumps({'auth_event': 'success'})  # TODO: think about where to put those success/failure messages
        )
    else:
        websocket.close()
