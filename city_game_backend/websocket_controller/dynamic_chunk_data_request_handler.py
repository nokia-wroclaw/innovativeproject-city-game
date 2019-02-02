from game_map.models import Structure, Chunk
from websocket_controller.message_utils import round_down, error_message, require_message_content
from city_game_backend import CONSTANTS
from game_map.utils import struct_2_dict
# from django.core import serializers
import json
from websocket_controller.WebsocketRoutes import WebsocketRoutes
from city_game_backend import CONSTANTS


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_DYNAMIC_CHUNK_DATA_REQUEST)
@require_message_content(
    ('lat', float),
    ('lon', float)
)
def handle_dynamic_chunk_data_request(message, websocket) -> str:
    chunk_lat = round_down(
        message['lat']
    )

    chunk_lon = round_down(
        message['lon']
    )

    requested_chunk: Chunk = Chunk.objects.filter(
        latitude_lower_bound=chunk_lat,
        longitude_lower_bound=chunk_lon
    ).first()

    if requested_chunk is None:
        return error_message('No chunk at given coordinates')

    requested_structures: [Structure] = Structure.objects.filter(
        chunk=requested_chunk
    )

    # This returns way too much data and produces output that
    # is incompatible with the Unity's deserialization system,
    # so there is a replacement below
    """
    return serializers.serialize('json', structures, fields=(
        'latitude', 'longitude', 'taken_over', 'owner', 'tier', 'resource_type', 'resources_left'
    ))
    """

    structures_to_send = [struct_2_dict(struct) for struct in requested_structures]

    response = {
        'structures': structures_to_send
    }
    return json.dumps(response)

