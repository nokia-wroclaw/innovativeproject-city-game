import math
import json

from player_manager.models import ActivePlayer
from city_game_backend.CONSTANTS import CHUNK_SIZE
from websocket_controller.active_connections_storage import ActiveConnectionsStorage
from city_game_backend import CONSTANTS

def round_down(n):
    return math.floor(n * 100) / 100


def notify_dynamic_map_structure_change(structure):
    lower_longitude = round_down(structure.longitude) - CHUNK_SIZE
    lower_latitude = round_down(structure.latitude) - CHUNK_SIZE

    upper_longitude = round_down(structure.longitude) + 2 * CHUNK_SIZE
    upper_latitude = round_down(structure.latitude) + 2 * CHUNK_SIZE

    notification_receivers = ActivePlayer.objects.filter(
        longitude__lte=upper_longitude,
        latitude__lte=upper_latitude,

        longitude__gte=lower_longitude,
        latitude__gte=lower_latitude
    )

    message_to_send: str = json.dumps({
        'id': CONSTANTS.SPECIAL_MESSAGE_MAP_UPDATE,
        'message': json.dumps({
            'structures': [struct_2_dict(structure)] # This has to be in an array, so it can be used by the same client callback as the dynamic structures data request
        })
    })

    for receiver in notification_receivers:
        ActiveConnectionsStorage.get(receiver.player.user.id).send(message_to_send)


def struct_2_dict(struct):
    """
    I am currently not satisfied with how the django serializers work,
    so I wrote this little converter

    :return: the structure formatted as dict, prepared to send into Unity
    """
    return {
        'id': struct.pk,

        'lat': struct.latitude,
        'lon': struct.longitude,

        'taken_over': struct.taken_over,
        'owner': struct.owner.nickname if struct.taken_over else '',  # Is this spaghetti ?

        'resource_type': struct.resource_type,
        'resources_left': struct.resources_left
    }
