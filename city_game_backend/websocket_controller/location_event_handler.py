import logging
from .message_utils import SUCCESS_MESSAGE, require_message_content
from player_manager.models import ActivePlayer

logger = logging.getLogger(__name__)

MAP_DATA_MESSAGE_TYPE = 'map_data'


@require_message_content(
    ('lat', float),
    ('lon', float)
)
def handle_location_event(message: dict, websocket):
    new_longitude = float(message['lon'])
    new_latitude = float(message['lat'])

    # Update the player's position in the database

    active_player_data: ActivePlayer = ActivePlayer.get_from_player_id(websocket.player_id)

    active_player_data.longitude = new_longitude
    active_player_data.latitude = new_latitude

    active_player_data.save()

    return SUCCESS_MESSAGE
