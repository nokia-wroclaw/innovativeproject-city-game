import math
from game_map.models import Chunk
from .message_utils import error_message


def handle_chunk_request(message, websocket) -> str:
    """
    Retrieve the chunk data for a given location
    """

    try:
        chunk_lat = round_down(
            message['lat']
        )

        chunk_lon = round_down(
            message['lon']
        )
    except KeyError or TypeError:
        return error_message('Wrong lat/lon data')

    chunk_to_send: Chunk = Chunk.objects.filter(
        latitude_lower_bound=chunk_lat,
        longitude_lower_bound=chunk_lon
    ).first()

    if chunk_to_send is None:
        # Todo: generate a chunk instead of sending empty data
        return "[]"

    return chunk_to_send.roads


def round_down(n):
    return math.floor(n * 100) / 100

