from channels.generic.websocket import WebsocketConsumer
import json
import logging
from game_map.models import Chunk, RoadNode
from django.core import serializers

logger = logging.getLogger(__name__)


class ClientCommunicationConsumer(WebsocketConsumer):
    def connect(self):
        logger.info('New websocket connection')
        self.accept()

    def disconnect(self, close_code):
        logger.info('Websocket disconnected!')
        pass

    def receive(self, text_data):
        logger.info(text_data)
        text_data_json = json.loads(text_data)
        message_type = text_data_json['type']

        if message_type == 'get_map':
            correct_chunk: Chunk = Chunk.objects.filter(
                latitude_lower_bound__gte=float(text_data_json['lat'])
            ).filter(
                longitude_lower_bound__gte=float(text_data_json['lon'])
            ).first()

            road_nodes = correct_chunk.roadnode_set.all()

            # TODO: HEY I'M HERE AND YOU NEED TO FIX THE SERIALIZERS
            response = {'map_data': road_nodes}
            self.send(text_data=serializers.serialize('json', road_nodes))