from game_map.models import Chunk
import logging
import math

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
        # TODO: IMPLEMENT AUTOMATIC CHUNK GENERATION HERE
        logger.critical('No chunk at this position, cannot generate new chunk at the moment!')
        return

    if websocket.active_player_data.on_which_chunk is None or \
            websocket.active_player_data.on_which_chunk.pk != on_which_chunk_now.pk:
        # handle_chunk_change() # TODO: IMPLEMENT
        websocket.active_player_data.on_which_chunk = on_which_chunk_now

    logger.debug(f'Saving new coordinates for {websocket.player.nickname}')
    websocket.active_player_data.latitude = new_latitude
    websocket.active_player_data.longitude= new_longitude
    websocket.active_player_data.save()

    # TODO: THIS LOOKS UGLY BUT WORKS AS INTENDED, CHECK FOR A CLEANER WAY
    # Explanation: I only want road_nodes to keep the ROADS DATA, not the chunk_id
    # or the message_type (see, below). Another thing - deserializing the JSON just to add two keys
    # and then serialize it back is a waste of time. I hope that justifies the code below
    websocket.send(
        text_data=
        '{"message_type": "' +
        MAP_DATA_MESSAGE_TYPE +
        '",  "chunk_id":' +
        str(on_which_chunk_now.id) +
        ', "road_nodes":' +
        on_which_chunk_now.road_nodes +
        '}')


def round_down(n):
    return math.floor(n * 100) / 100
