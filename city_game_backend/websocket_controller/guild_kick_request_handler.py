from websocket_controller.message_utils import require_message_content, error_message, SUCCESS_MESSAGE
from guild_manager.models import Guild
from player_manager.models import Player
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes
from websocket_controller.active_connections_storage import ActiveConnectionsStorage
import json


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_SEND_GUILD_KICK_REQUEST)
@require_message_content(
    ('nick_to_kick', str)
)
def handle_kick_request(message, websocket) -> str:
    sender: Player = Player.get_by_id(websocket.player_id)
    player_to_be_kicked: Player = Player.get_by_nick(message['nick_to_kick'])

    if player_to_be_kicked is None:
        return error_message('No player with that nick found :(')

    if sender.guild == player_to_be_kicked.guild and sender.guild is not None:
        Guild.remove_player_from_guild(player_to_be_kicked)
        return SUCCESS_MESSAGE

    else:
        return error_message('Cannot kick that player...    ')

