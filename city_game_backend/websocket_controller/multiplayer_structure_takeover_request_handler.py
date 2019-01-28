from websocket_controller.message_utils import require_message_content, error_message, SUCCESS_MESSAGE
from game_map.models import Structure
from game_map.utils import notify_dynamic_map_structure_change
from player_manager.models import Player, ActivePlayer
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes

twenty_meters_distance = 0.002  # Approximately 20 meters in the real world (in latitude/longitude units)
# TODO: CREATE A MODULE THAT CONVERTS LENGTH UNITS


@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_MULTIPLAYER_STRUCT_TAKEOVER_REQUEST)
@require_message_content(
    ('id', int)
)
def handle_multiplayer_structure_takeover_request(message, websocket) -> str:
    """
    For now, structure claiming JUST WORKS, no safety checks implemented
    TODO: SECURE THIS.. but focus on more important stuff first (like game UI)
    """

    new_potential_owner: Player = Player.get_by_id(websocket.player_id)
    new_potential_owner_location_data: ActivePlayer = ActivePlayer.get_from_player_id(websocket.player_id)

    if new_potential_owner.guild is None:
        return error_message('You have no guild..')


    structure_to_claim_id = message['id']
    structure_to_claim: Structure = Structure.objects.filter(
        id=structure_to_claim_id
    ).first()

    if structure_to_claim is None:
        return error_message(f'No struct with a given id: {structure_to_claim_id}')

    # TODO: FINISH THIS
    '''
    players_helping = ActivePlayer.objects.filter(
        player__guild=new_potential_owner.guild,
        latitude__gt=new_potential_owner_location_data.latitude - twenty_meters_distance,
        latitude__lt=new_potential_owner_location_data.latitude + twenty_meters_distance,
        longitude__gt=new_potential_owner_location_data.longitude - twenty_meters_distance,
        longitude__lt=new_potential_owner_location_data.longitude + twenty_meters_distance
    ).all().aggregate()
    '''

    structure_to_claim.taken_over = True
    structure_to_claim.taken_over_by_guild = True

    structure_to_claim.owner = new_potential_owner
    structure_to_claim.save()
    notify_dynamic_map_structure_change(structure_to_claim)
    return SUCCESS_MESSAGE
