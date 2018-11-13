from .models import Chunk

import requests
import xml.etree.ElementTree
import json

import logging

logger = logging.getLogger(__name__)


OVERPASS_API_URL = 'https://lz4.overpass-api.de/api/interpreter'
CHUNK_SIZE = 0.01  # About 111x111 meters
STANDARD_QUERY = '''
way
  (
{}, {},
{}, {}
)

["highway"~"primary|secondary|tertiary|residential|living_street|service"];

(._;>;);

out;

'''


def perform_overpass_query(query: str) -> str:
    """
    Performs an overpass API query and returns its response

    :param query: overpass query in the overpass query language
    :return: the overpass api response
    """
    data = {'data': query}

    logger.debug('Executing a following Overpass query: {}'.format(query))

    request = requests.post(OVERPASS_API_URL, data=data)

    return request.content.decode()


def save_one_chunk(xml_data: str, lower_latitude: float, lower_longitude: float):
    """
    Loads the XML generated by the `perform_overpass_query` to the database

    :param xml_data: the xml data from the overpass api
    :param lower_latitude: the bottom border of the chunk
    :param lower_longitude: the left border of the chunk
    """
    root = xml.etree.ElementTree.fromstring(xml_data)

    new_chunk = Chunk(
        latitude_lower_bound=lower_latitude,
        longitude_lower_bound=lower_longitude,
        latitude_upper_bound=lower_latitude + CHUNK_SIZE,
        longitude_upper_bound=lower_longitude + CHUNK_SIZE
    )

    # Will be used to store all the nodes of all roads, later saved to the Chunk object in the DB as a JSON
    roads = []

    # Going through all the roads - collections of points inside the XML
    for road in root.findall('way'):

        # Each road is a list of points
        road_nodes = []

        # the <way> point collections don't contain the road points coordintates, they just contain a reference
        # to a specific <node id="reference">
        for road_point in road.findall('nd'):
            ref = road_point.get('ref')

            # Looks for every element that
            query = ".//*[@id='{}']".format(ref)

            node_next_list = root.findall(query)

            try:
                node_next = node_next_list[0]

            except IndexError:
                logger.warning('Could not found a road node with id: {}, skipping it'.format(ref))
                continue

            road_nodes.append(
                {
                    'lon': node_next.get('lon'),
                    'lat': node_next.get('lat')
                }
            )

        roads.append(
            {
                'nodes': road_nodes
            }
        )

    new_chunk.roads = json.dumps({
        'roads': roads,
        'latitude_lower_bound': lower_latitude,
        'longitude_lower_bound': lower_longitude
    })
    new_chunk.save()


# chunk_generator.batch_chunks_loading(51.07, 17.00, 20) is a cool place to start, generates the whole Wrocław
def batch_chunks_loading(lower_longitude_start, lower_latitude_start, square_size):
    """
    Generates map chunks starting from the given latitude and longitude,
    goes towards top and right generating a square with a given size

    EXAMPLE:

    square_size - 5
    lower_lat - 50
    lower_long - 17

     _ _ _ _
    |_|_|_|_|
    |_|_|_|_|   The square side is 5 chunks long, so 25 chunks were generated
    |_|_|_|_|
    |_|_|_|_|
    |_|_|_|_|

    ^ the lower left corner is your starting point - (lower_lat, lower_long)
    """

    for x_offset in range(square_size):
        for y_offset in range(square_size):
            # Converting the offsets to 'chunk units'
            chunk_x_offset = x_offset * CHUNK_SIZE
            chunk_y_offset = y_offset * CHUNK_SIZE

            logger.info(
                'Downloading data for chunk {}, {}'.format(
                    lower_longitude_start + chunk_y_offset,
                    lower_latitude_start + chunk_x_offset
                )
            )

            query = STANDARD_QUERY.format(
                lower_longitude_start + chunk_y_offset,
                lower_latitude_start + chunk_x_offset,
                lower_longitude_start + chunk_y_offset + CHUNK_SIZE,
                lower_latitude_start + chunk_x_offset + CHUNK_SIZE
            )
            data = perform_overpass_query(query)

            logger.debug(
                '\tSaving chunk {}, {}'.format(
                    lower_longitude_start + chunk_y_offset,
                    lower_latitude_start + chunk_x_offset
                )
            )

            save_one_chunk(
                data,
                lower_longitude_start + chunk_y_offset,
                lower_latitude_start + chunk_x_offset
            )

            logger.debug(
                '\tData chunk {}, {} saved!'.format(
                    lower_longitude_start + chunk_y_offset,
                    lower_latitude_start + chunk_x_offset
                )
            )
