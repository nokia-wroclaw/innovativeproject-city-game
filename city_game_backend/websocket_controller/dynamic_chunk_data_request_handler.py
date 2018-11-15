from game_map.models import Structure, Chunk
from .message_utils import round_down, error_message
from city_game_backend import CONSTANTS
# from django.core import serializers
import json

def handle_dynamic_chunk_data_request(message, websocket) -> str:
    try:
        chunk_lat = round_down(
            message['lat']
        )

        chunk_lon = round_down(
            message['lon']
        )
    except KeyError or TypeError:
        return error_message('Wrong lat/lon data')

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

    structures_to_send = [
        {
            'id': struct.pk,

            'lat': struct.latitude,
            'lon': struct.longitude,

            'taken_over': struct.taken_over,
            'owner': struct.owner.nickname if struct.taken_over else '',  # Is this spaghetti ?

            'resource_type': struct.resource_type,
            'resources_left': struct.resources_left
        }
        for struct in requested_structures
    ]

    response = {
        'structures': structures_to_send
    }
    return json.dumps(response)

