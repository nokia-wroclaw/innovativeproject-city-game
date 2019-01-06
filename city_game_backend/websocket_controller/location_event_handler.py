import logging
from .message_utils import SUCCESS_MESSAGE, require_message_content
from player_manager.models import ActivePlayer
from guild_manager.utils import notify_players_about_guild_member_position_change

logger = logging.getLogger(__name__)

MAP_DATA_MESSAGE_TYPE = 'map_data'


@require_message_content(
    ('lat', float),
    ('lon', float),
    ('rotation', float)
)
def handle_location_event(message: dict, websocket):
    new_longitude = float(message['lon'])
    new_latitude = float(message['lat'])
    new_rotation = float(message['rotation'])

    # Update the player's position in the database

    active_player: ActivePlayer = ActivePlayer.get_from_player_id(websocket.player_id)

    active_player.longitude = new_longitude
    active_player.latitude = new_latitude
    active_player.rotation = new_rotation

    active_player.save()
    notify_players_about_guild_member_position_change(active_player)

    return SUCCESS_MESSAGE
