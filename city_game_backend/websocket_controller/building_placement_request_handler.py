from .message_utils import SUCCESS_MESSAGE, require_message_content
from game_map.models import Structure
from city_game_backend import CONSTANTS
from player_manager.models import Player
from game_map.utils import notify_dynamic_map_structure_change


@require_message_content(
    ('lat', float),
    ('lon', float),
    ('rotation', float),
    ('tier', int)
)
def handle_building_placement_request(message: dict, websocket) -> str:
    new_struct_lat = message['lat']
    new_struct_lon = message['lon']
    new_struct_rotation = message['rotation']
    new_struct_tier = message['tier']

    new_structure = Structure()
    new_structure.latitude = new_struct_lat
    new_structure.longitude = new_struct_lon
    new_structure.rotation = new_struct_rotation
    new_structure.tier = new_struct_tier
    new_structure.resource_type = CONSTANTS.RESOURCE_TYPE_4  # AoE buff resource type
    new_structure.owner = Player.get_by_id(websocket.player_id)


    new_structure.save()
    notify_dynamic_map_structure_change(new_structure)

    return SUCCESS_MESSAGE
