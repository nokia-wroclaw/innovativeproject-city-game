import math
from game_map.models import Chunk
from websocket_controller.message_utils import error_message, round_down, require_message_content
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_CHUNK_REQUEST)
@require_message_content(
    ('lat', float),
    ('lon', float)
)
def handle_chunk_request(message, websocket) -> str:
    """
    Retrieve the chunk data for a given location
    """

    chunk_lat = round_down(
        message['lat']
    )

    chunk_lon = round_down(
        message['lon']
    )

    chunk_to_send: Chunk = Chunk.objects.filter(
        latitude_lower_bound=chunk_lat,
        longitude_lower_bound=chunk_lon
    ).first()

    if chunk_to_send is None:
        # Todo: generate a chunk instead of sending empty data
        return "[]"

    #print(chunk_to_send.roads)
    return chunk_to_send.roads



