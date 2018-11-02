from enum import Enum


class MessageType(Enum):
    AUTH_EVENT = 0
    LOCATION_EVENT = 1
    CHUNK_REQUEST = 2
