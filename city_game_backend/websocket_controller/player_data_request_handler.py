import json
from player_manager.models import Player
from game_map.utils import notify_dynamic_map_structure_change
from city_game_backend import CONSTANTS
from .WebsocketRoutes import WebsocketRoutes
from guild_manager.models import GuildInvite


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_PLAYER_DATA_REQUEST)
def handle_player_data_request(message: dict, websocket) -> str:
    player_to_return = Player.get_by_id(websocket.player_id)

    player_guild_invites = GuildInvite.get_invites_of_player(player_to_return)

    return_data = {
        'name': 'error',
        'level': 0,
        'exp': 0,
        CONSTANTS.RESOURCE_CEMENTIA: 0,
        CONSTANTS.RESOURCE_PLASMATIA: 0,
        CONSTANTS.RESOURCE_AUFERIA: 0,
        'guild': None,

        'invites': []

    }
    try:
        return_data = {
            'name': player_to_return.nickname,
            'level': player_to_return.level,
            'exp': player_to_return.exp,
            CONSTANTS.RESOURCE_CEMENTIA: player_to_return.Cementia,
            CONSTANTS.RESOURCE_PLASMATIA: player_to_return.Plasmatia,
            CONSTANTS.RESOURCE_AUFERIA: player_to_return.Auferia,
            'guild': player_to_return.guild.guild_name if player_to_return.guild is not None else None,

            'invites': [
                {
                    'guild_name': invite.guild.guild_name,
                    'invite_id': invite.id
                }
                for invite in player_guild_invites
            ]

        }
    except:
        pass

    #print(json.dumps(return_data))
    return json.dumps(return_data)
