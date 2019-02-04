import math
from game_map.models import Chunk
from guild_manager.models import Guild
from websocket_controller.message_utils import error_message, round_down, require_message_content
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes
import json

@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_CHUNK_OWNER_REQUEST)
@require_message_content(
    ('lat', float),
    ('lon', float)
)
def handle_chunk_owner_request(message, websocket) -> str:
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
        return error_message("No chunk!")

    owner: Guild = chunk_to_send.owner_guild

    if owner is not None:
        message = {
            'chunk_id': chunk_to_send.id,
            'lon_lower': chunk_to_send.longitude_lower_bound,
            'lat_lower': chunk_to_send.latitude_lower_bound,

            'owner_guild': owner.guild_name,
            'color': owner.color,
            'icon': owner.icon
        }
    else:
        message = {
            'chunk_id': chunk_to_send.id,
            'lon_lower': chunk_to_send.longitude_lower_bound,
            'lat_lower': chunk_to_send.latitude_lower_bound,

            'owner_guild': None,
            'color': None,
            'icon': None
        }

    return json.dumps(message)



