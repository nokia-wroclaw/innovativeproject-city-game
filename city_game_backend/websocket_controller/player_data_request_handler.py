import json
from player_manager.models import Player
from game_map.utils import notify_dynamic_map_structure_change
from city_game_backend import CONSTANTS


def handle_player_data_request(message: dict, websocket) -> str:
    player_to_return = Player.get_by_id(websocket.player_id)

    return_data = {
        'level': player_to_return.level,
        'exp': player_to_return.exp,
        CONSTANTS.RESOURCE_CEMENTIA: player_to_return.Cementia,
        CONSTANTS.RESOURCE_PLASMATIA: player_to_return.Plasmatia,
        CONSTANTS.RESOURCE_AUFERIA: player_to_return.Auferia,
        'guild': player_to_return.guild.guild_name if player_to_return.guild is not None else None
    }

    return json.dumps(return_data)
