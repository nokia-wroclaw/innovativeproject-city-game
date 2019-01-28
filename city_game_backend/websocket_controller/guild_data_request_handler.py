import json
from player_manager.models import Player
from guild_manager.models import Guild
from game_map.utils import notify_dynamic_map_structure_change
from websocket_controller import WebsocketRoutes
from websocket_controller.message_utils import error_message, SUCCESS_MESSAGE
from city_game_backend import CONSTANTS
from .WebsocketRoutes import WebsocketRoutes


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_GUILD_DATA_REQUEST)
def handle_guild_data_request(message, websocket) -> str:
    player: Player = Player.get_by_id(websocket.player_id)
    guild: Guild = player.guild

    if guild is None:
        return error_message("No guild")

    guild_data = {
        'name': guild.guild_name,
        'members_count': guild.members_count,
        'members': [player.nickname for player in guild.player_set.all()]
    }

    return json.dumps(guild_data)


