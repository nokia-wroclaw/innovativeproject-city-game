from .message_utils import require_message_content, SUCCESS_MESSAGE
from city_game_backend import CONSTANTS
from player_manager.models import Player
from guild_manager.models import Guild
from city_game_backend import CONSTANTS
from .WebsocketRoutes import WebsocketRoutes


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_CREATE_GUILD)
@require_message_content(
    ('guild_name', str)
)
def handle_guild_creation_request(message, websocket) -> str:
    player: Player = Player.get_by_id(websocket.player_id)

    Guild.remove_player_from_guild(player)

    guild_name = message['guild_name']
    new_guild = Guild()
    
    new_guild.guild_name = guild_name
    new_guild.save()

    new_guild.add_player(player)

    return SUCCESS_MESSAGE
