from websocket_controller.message_utils import require_message_content, error_message, SUCCESS_MESSAGE
from guild_manager.models import GuildInvite
from player_manager.models import Player
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes
from websocket_controller.active_connections_storage import ActiveConnectionsStorage
import json

@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_SEND_GUILD_INVITE)
@require_message_content(
    ('receiver', str)
)
def handle_guild_invite_response_request(message, websocket) -> str:
    sender: Player = Player.get_by_id(websocket.player_id)
    receiver: Player = Player.get_by_nick(message['receiver'])

    if receiver is None:
        return error_message('No player with that nick found :(')

    if sender.guild is None:
        return error_message('How can you even send it, you have no guild..')

    new_invite = GuildInvite()
    new_invite.guild = sender.guild
    new_invite.receiver = receiver
    new_invite.save()

    send_invitation_notification(new_invite)

    return SUCCESS_MESSAGE


def send_invitation_notification(invitation: GuildInvite):
    receiver_id = invitation.receiver.id

    receivers_websocket = ActiveConnectionsStorage.get(receiver_id)

    if receivers_websocket is None:
        return

    message: str = json.dumps({
        'id': CONSTANTS.SPECIAL_MESSAGE_GUILD_INVITE_NOTIFICATION,
        'message': json.dumps({
            'guild_name': invitation.guild.guild_name,
            'invite_id': invitation.id
        })
    })

    receivers_websocket.send(
        message
    )