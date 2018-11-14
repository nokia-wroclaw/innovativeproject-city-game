import logging
from .message_utils import SUCCESS_MESSAGE


logger = logging.getLogger(__name__)

MAP_DATA_MESSAGE_TYPE = 'map_data'


def handle_location_event(message: dict, websocket):
    new_longitude = float(message['lon'])
    new_latitude = float(message['lat'])

    # Update the player's position in the database
    websocket.active_player_data.longitude = new_longitude
    websocket.active_player_data.latitude = new_latitude

    websocket.active_player_data.save()

    return SUCCESS_MESSAGE
