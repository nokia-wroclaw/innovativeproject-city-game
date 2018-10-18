from channels.generic.websocket import WebsocketConsumer
# TODO: check out JsonWebsocketConsumer or AsyncJsonWebsocketConsumer after we gather more info about django-channels
# Now just get it to work as intended

import json
import logging
from .location_event_handler import handle_location_event
from .auth_event_handler import handle_auth_event
from .disconnect_event_handler import handle_disconnect_event

logger = logging.getLogger(__name__)

AUTH_EVENT = 'auth_event'
LOCATION_EVENT = 'location_event'


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
            print(f'NOT A JSON MESSAGE: {text_data}')
            # TODO: should a websocket close after receiving an invalid message?
            self.close()
            return

        message_type = message['type']

        # If user is not authenticated, we only let him to send an auth message
        if self.user is None:
            if message_type == AUTH_EVENT:
                handle_auth_event(message, self)
            else:  # TODO: IMPLEMENT SOME SORT OF LOGIN TIMEOUT INSTEAD OF WAITING FOR A MESSAGE
                self.close()

        # If the user is authenticated, other actions are available to him
        if message_type == LOCATION_EVENT:
            handle_location_event(message, self)
