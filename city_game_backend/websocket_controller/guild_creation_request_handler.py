from .message_utils import require_message_content
from city_game_backend import CONSTANTS

@require_message_content(
    ('guild_name', str)
)
def handle_guild_creation_request(message, websocket) -> str:
    """
        Handle for each basic action involving a guild:
            * creating a new one
            * removing an existing one
            * adding a player to a guild
            * removing a player from a guild
    """

    guild_name = message['guild_name']
