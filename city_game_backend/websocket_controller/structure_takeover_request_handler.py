from .message_utils import require_message_content, error_message, SUCCESS_MESSAGE
from game_map.models import Structure
from game_map.utils import notify_dynamic_map_structure_change
from player_manager.models import Player
from city_game_backend import CONSTANTS
from .WebsocketRoutes import WebsocketRoutes


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_STRUCT_TAKEOVER_REQUEST)
@require_message_content(
    ('id', int)
)
def handle_structure_takeover_request(message, websocket) -> str:
    """
    For now, structure claiming JUST WORKS, no safety checks implemented
    TODO: SECURE THIS.. but focus on more important stuff first (like game UI)
    """
    id = message['id']
    structure_to_claim: Structure = Structure.objects.filter(
        id=id
    ).first()

    if structure_to_claim is None:
        return error_message(f'No struct with a given id: {id}')

    structure_to_claim.taken_over = True
    structure_to_claim.owner = Player.get_by_id(websocket.player_id) 
    structure_to_claim.save()
    notify_dynamic_map_structure_change(structure_to_claim)
    return SUCCESS_MESSAGE
