from game_map.models import Chunk
from game_map.chunk_generator import CHUNK_SIZE

import logging
import math
from .message_utils import SUCCESS_MESSAGE


logger = logging.getLogger(__name__)

MAP_DATA_MESSAGE_TYPE = 'map_data'


def handle_location_event(message: dict, websocket):
    new_longitude = float(message['lon'])
    new_latitude = float(message['lat'])

    # Update the player's position in the database
    websocket.active_player_data.longitude = new_longitude
    websocket.active_player_data.latitude = new_latitude

    chunk_latitude = round_down(new_latitude)
    chunk_longitude = round_down(new_longitude)

    print(f'looking for {chunk_longitude}, {chunk_latitude}')

    on_which_chunk_now: Chunk = Chunk.objects.filter(
        latitude_lower_bound__gte=chunk_latitude
    ).filter(
        longitude_lower_bound__gte=chunk_longitude
    ).first()

    if on_which_chunk_now is None:
        logger.critical('No chunk at this position, cannot generate new chunk at the moment!')
        return

    if websocket.active_player_data.on_which_chunk is None or \
            websocket.active_player_data.on_which_chunk.pk != on_which_chunk_now.pk:
        handle_chunk_change(on_which_chunk_now, websocket)
        websocket.active_player_data.on_which_chunk = on_which_chunk_now

    logger.debug(f'Saving new coordinates for {websocket.player.nickname}')
    websocket.active_player_data.latitude = new_latitude
    websocket.active_player_data.longitude = new_longitude
    websocket.active_player_data.save()

    return SUCCESS_MESSAGE


def round_down(n):
    return math.floor(n * 100) / 100


def handle_chunk_change(on_which_chunk_now, websocket) -> None:
    return
    """
    DEPRECATED: THINKING FOR A NEW SYSTEM FOR CHUNK REQUESTING
    
    Sends the chunk data to the client if he moves into a different chunk
    """

    # We will download
    # * lower left chunk
    # * lower middle chunk
    # * lower right chunk
    # * middle right chunk
    # * middle middle chunk (the chunk the player is on)
    # etc etc, a square around player's coordinates

    middle_chunk_longitude = on_which_chunk_now.longitude_lower_bound
    middle_chunk_latitude = on_which_chunk_now.latitude_lower_bound

    longitude_limits = [
            middle_chunk_longitude - CHUNK_SIZE,
            middle_chunk_longitude,
            middle_chunk_longitude + CHUNK_SIZE
        ]

    latitude_limits = [
            middle_chunk_latitude - CHUNK_SIZE,
            middle_chunk_latitude,
            middle_chunk_latitude + CHUNK_SIZE
    ]

    logger.debug(f'searching across lons: {longitude_limits} and lats: {latitude_limits}')

    chunks = Chunk.objects.filter(
        longitude_lower_bound__in=longitude_limits
    ).filter(
        latitude_lower_bound__in=latitude_limits
    )

    logger.debug(f'found {len(chunks)} chunks!')

    # TODO: THIS LOOKS TERRIBLY WRONG BUT WORKS AS INTENDED, WE NEED TO FIND A CLEANER WAY
    # Explanation: I only want road_nodes to keep the ROADS DATA, not the chunk_id
    # or the message_type (see, below). Another thing - deserializing the JSON just to add two keys
    # and then serialize it back is a waste of time. I hope that justifies the code below

    chunks_data = ['{ "id": ' + str(chunk.pk) + ',"road_nodes": ' + chunk.road_nodes + '}' for chunk in chunks]

    # TIL, JSON won't parse an array that looks like this [1,2,3,4,] - there cannot be a comma in the end
    for i in range(len(chunks_data)-1):
        chunks_data[i] += ','

    chunks_data_message = '{"message_type": "' + \
        MAP_DATA_MESSAGE_TYPE + \
        '",  "chunks_data": ['

    for chunk_data in chunks_data:
        chunks_data_message += chunk_data
    chunks_data_message += ']}'

    websocket.send(chunks_data_message)

    '''
    websocket.send(
        text_data=
        '{"message_type": "' +
        MAP_DATA_MESSAGE_TYPE +
        '",  "chunks_data":' +
        '[{}]'.format(*chunks_data) +
        '}')
    '''

