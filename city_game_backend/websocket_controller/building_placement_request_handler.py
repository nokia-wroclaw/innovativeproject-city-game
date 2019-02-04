from django.db.models import Max

from websocket_controller.message_utils import SUCCESS_MESSAGE, require_message_content
from game_map.models import Structure, Chunk
from player_manager.models import Player, ActivePlayer
from guild_manager.models import Guild
from game_map.utils import notify_dynamic_map_structure_change
from city_game_backend import CONSTANTS
from websocket_controller.WebsocketRoutes import WebsocketRoutes
import json
from websocket_controller.active_connections_storage import ActiveConnectionsStorage

@WebsocketRoutes.route(CONSTANTS.MESSAGE_TYPE_STRUCT_PLACEMENT_REQUEST)
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

    owner: Player = Player.get_by_id(websocket.player_id)

    new_structure = Structure()
    new_structure.latitude = new_struct_lat
    new_structure.longitude = new_struct_lon
    new_structure.rotation = new_struct_rotation
    new_structure.tier = new_struct_tier
    new_structure.resource_type = CONSTANTS.RESOURCE_TYPE_4  # AoE buff resource type
    new_structure.owner = owner
    new_structure.owner_guild = owner.guild
    new_structure.taken_over_by_guild = True
    new_structure.taken_over = True
    new_structure.save()
    notify_dynamic_map_structure_change(new_structure)

    recalculate_chunk_owner(new_structure.chunk)

    return SUCCESS_MESSAGE


def recalculate_chunk_owner(chunk: Chunk):
    top_guild = Structure.objects.filter(
        chunk=chunk, taken_over_by_guild=True
    ).values('owner_guild').annotate(
        power=Max('tier')).order_by('-power').first()

    if top_guild is not None:
        top_guild = Guild.objects.filter(
            id=top_guild['owner_guild']
        ).first()
        print(f'setting {chunk.id}\'s owner to {top_guild}!')
    else:
        print(f'setting {chunk.id}\'s owner to none!')

    chunk.owner_guild = top_guild
    chunk.save()

    send_chunk_owner_update_notification(chunk)


def send_chunk_owner_update_notification(chunk: Chunk):

    notification_receivers = ActivePlayer.objects.filter(
        longitude__lte=chunk.longitude_upper_bound + CONSTANTS.CHUNK_SIZE*3,
        latitude__lte=chunk.latitude_upper_bound + CONSTANTS.CHUNK_SIZE*3,

        longitude__gte=chunk.longitude_lower_bound - CONSTANTS.CHUNK_SIZE*3,
        latitude__gte=chunk.latitude_lower_bound - CONSTANTS.CHUNK_SIZE*3
    )

    if chunk.owner_guild is not None:
        message_to_send: str = json.dumps({
            'id': CONSTANTS.SPECIAL_MESSAGE_CHUNK_OWNER_CHANGE_NOTIFICATION,
            'message': json.dumps({
                'chunk_id': chunk.id,
                'lon_lower': chunk.longitude_lower_bound,
                'lat_lower': chunk.latitude_lower_bound,

                'owner_guild': chunk.owner_guild.guild_name,
                'color': chunk.owner_guild.color,
                'icon': chunk.owner_guild.icon
            })
        })
    else:
        message_to_send: str = json.dumps({
            'id': CONSTANTS.SPECIAL_MESSAGE_CHUNK_OWNER_CHANGE_NOTIFICATION,
            'message': json.dumps({
                'chunk_id': chunk.id,
                'lon_lower': chunk.longitude_lower_bound,
                'lat_lower': chunk.latitude_lower_bound,

                'owner_guild': None,
                'color': None,
                'icon': None
            })
        })

    for receiver in notification_receivers:
        ActiveConnectionsStorage.get(receiver.player.id).send(message_to_send)

