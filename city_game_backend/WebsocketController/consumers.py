from channels.generic.websocket import WebsocketConsumer
import json
import logging
from MapChunksGenerator.models import Chunk, RoadNode
from django.core import serializers

logger = logging.getLogger(__name__)


class ChatConsumer(WebsocketConsumer):
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

            correct_chunk = Chunk.objects.filter(
                latitude_lower_bound__gte=float(text_data_json['lat'])
            ).filter(
                longitude_lower_bound__gte=float(text_data_json['lon'])
            ).first()
            correct_chunk: Chunk

            road_nodes = correct_chunk.roadnode_set.all()
            #print(road_nodes[0])

            self.send(text_data=json.dumps({
                'get_map': serializers.serialize('json', road_nodes)
            }))