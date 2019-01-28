from websocket_controller.message_utils import require_message_content, error_message, SUCCESS_MESSAGE
from guild_manager.models import GuildInvite
from player_manager.models import Player
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_SEND_GUILD_INVITE)
@require_message_content(
    ('answer', int),
    ('invite_id', int)
)
def handle_guild_invite_request(message, websocket) -> str:

    invite_id = message['invite_id']
    players_answer = message['answer']

    invite: GuildInvite = GuildInvite.get_by_id(invite_id)
    if invite is None:
        return error_message('Wrong invite ID!')

    if players_answer == CONSTANTS.GUILD_INVITE_DENY:
        invite.delete()
        return SUCCESS_MESSAGE

    elif players_answer == CONSTANTS.GUILD_INVITE_ACCEPT:
        receiver: Player = Player.get_by_id(websocket.player_id)
        invite.guild.add_player(receiver)
        return SUCCESS_MESSAGE

    else:
        return error_message('Incorrect invite response...')
