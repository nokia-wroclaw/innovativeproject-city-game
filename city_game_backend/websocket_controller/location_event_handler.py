from game_map.models import Chunk


MAP_DATA_MESSAGE_TYPE = 'map_data'


def handle_location_event(message: dict, websocket):
    correct_chunk: Chunk = Chunk.objects.filter(
        latitude_lower_bound__gte=float(message['lat'])
    ).filter(
        longitude_lower_bound__gte=float(message['lon'])
    ).first()

    print(correct_chunk.id)

    # TODO: THIS LOOKS UGLY BUT WORKS AS INTENDED, CHECK FOR A CLEANER WAY
    # Explanation: I only want road_nodes to keep the ROADS DATA, not the chunk_id
    # or the message_type (see, below). Another thing - deserializing the JSON just to add two keys
    # and then serialize it back is a waste of time. I hope that justifies the code below
    websocket.send(
        text_data=
        '{"message_type": "' +
        MAP_DATA_MESSAGE_TYPE +
        '",  "chunk_id":' +
        str(correct_chunk.id) +
        ', "road_nodes":' +
        correct_chunk.road_nodes +
        '}')
