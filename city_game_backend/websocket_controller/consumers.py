from channels.generic.websocket import WebsocketConsumer
# TODO: check out JsonWebsocketConsumer or AsyncJsonWebsocketConsumer after we gather more info about django-channels
# Now just get it to work as intended

import json
import logging
from game_map.models import Chunk

logger = logging.getLogger(__name__)

MAP_DATA_MESSAGE_TYPE = 'map_data'


class ActiveConnectionsStorage:
    """
    The class stores every active websocket connection,
    allows to search for a specific connection and will probably provide more helpers in the future
    """
    connections = {}

    @staticmethod
    def clear():
        for connection in ActiveConnectionsStorage.connections:
            connection.close()
        ActiveConnectionsStorage.connections = {}

    @staticmethod
    def get(client_id: int):
        if client_id in ActiveConnectionsStorage.connections:
            return ActiveConnectionsStorage.connections[client_id]
        else:
            return None


class ClientCommunicationConsumer(WebsocketConsumer):
    """
    The websocket connection used by EVERY player - it is used to login and talk to the game server
    """
    def connect(self):
        logger.info('New websocket connection')
        self.accept()

    def disconnect(self, close_code):
        logger.info('Websocket disconnected!')
        pass

    def receive(self, text_data):
        logger.info(text_data)
        try:
            text_data_json = json.loads(text_data)
        except json.JSONDecodeError:
            print(f'NOT A JSON MESSAGE: {text_data}')
            return

        message_type = text_data_json['type']

        if message_type == 'get_map':

            correct_chunk: Chunk = Chunk.objects.filter(
                latitude_lower_bound__gte=float(text_data_json['lat'])
            ).filter(
                longitude_lower_bound__gte=float(text_data_json['lon'])
            ).first()

            print(correct_chunk.id)

            # TODO: THIS LOOKS UGLY BUT WORKS AS INTENDED, CHECK FOR A CLEANER WAY
            # Explanation: I only want road_nodes to keep the ROADS DATA, not the chunk_id
            # or the message_type (see, below). Another thing - deserializing the JSON just to add two keys
            # and then serialize it back is a waste of time. I hope that justifies the code below
            self.send(text_data=
                      '{"message_type": "' +
                      MAP_DATA_MESSAGE_TYPE +
                      '",  "chunk_id":' +
                      str(correct_chunk.id) +
                      ', "road_nodes":' +
                      correct_chunk.road_nodes +
                      '}')
