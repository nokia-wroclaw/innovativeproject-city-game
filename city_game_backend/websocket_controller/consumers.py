from channels.generic.websocket import WebsocketConsumer
# TODO: check out JsonWebsocketConsumer or AsyncJsonWebsocketConsumer after we gather more info about django-channels
# Now just get it to work as intended

import json
import logging
from .location_event_handler import handle_location_event
from .auth_event_handler import handle_auth_event
from .disconnect_event_handler import handle_disconnect_event
from .message_type import MessageType
from .message_utils import error_message

logger = logging.getLogger(__name__)


class ClientCommunicationConsumer(WebsocketConsumer):
    """
    The websocket connection used by EVERY player - it is used to login and talk to the game server
    """

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.user = None  # Link to the user account
        self.player = None  # Link to the player data of this account
        self.activePlayerData = None  # Storage for player's location

    def connect(self):
        logger.info('New websocket connection')
        self.accept()

    def disconnect(self, close_code):
        handle_disconnect_event(self)
        logger.info('Websocket disconnected!')

    def receive(self, text_data):
        logger.debug(text_data)
        try:
            message = json.loads(text_data)
        except json.JSONDecodeError:
            self.send('Invalid json')
            self.close()
            return

        message_type = None
        transaction_id = None
        try:
            transaction_id = message['id']
            message_type = MessageType(int(message['type']))
            message = message['data']
        except KeyError:
            self.send(error_message('No message type/transaction id'))
            return

        response = {
            'id': transaction_id,
            'message': json.dumps(self.handle_message(message, message_type))
        }
        self.send(json.dumps(response))

    def handle_message(self, message: dict, message_type: MessageType) -> str:
        # If user is not authenticated, we only let him to send an auth message
        print('Handling', message_type.value)
        if self.user is None:
            if message_type == MessageType.AUTH_EVENT:
                return handle_auth_event(message, self)
            else:  # TODO: IMPLEMENT SOME SORT OF LOGIN TIMEOUT INSTEAD OF WAITING FOR A MESSAGE
                self.send('User not authorised')
                self.close()

        # If the user is authenticated, other actions are available to him
        if message_type == MessageType.LOCATION_EVENT:
            return handle_location_event(message, self)
        # ... another message type handlers here ...
        else:
            self.send('Wrong message type')
