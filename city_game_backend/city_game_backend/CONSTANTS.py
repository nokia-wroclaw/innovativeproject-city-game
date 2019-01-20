# Connection specific
SERVER_URL = "filipplotnicki.com:8000/ws/"


# Map specific
CHUNK_SIZE = 0.01


# Client-driven message types
MESSAGE_TYPE_AUTH_EVENT = 0
MESSAGE_TYPE_LOCATION_EVENT = 1
MESSAGE_TYPE_CHUNK_REQUEST = 2
MESSAGE_TYPE_DYNAMIC_CHUNK_DATA_REQUEST = 3
MESSAGE_TYPE_STRUCT_TAKEOVER_REQUEST = 4
MESSAGE_TYPE_CREATE_GUILD = 5
MESSAGE_TYPE_STRUCT_PLACEMENT_REQUEST = 6
MESSAGE_TYPE_PLAYER_DATA_REQUEST = 7

# Server-driven (also referred to as special) message types
SPECIAL_MESSAGE_MAP_UPDATE = 0
SPECIAL_MESSAGE_NOTIFICATION = 1
SPECIAL_MESSAGE_GUILD_MEMBER_POSITION_UPDATE = 2

# Resource types
RESOURCE_TYPE_1 = 1
RESOURCE_TYPE_2 = 2
RESOURCE_TYPE_3 = 3
RESOURCE_TYPE_4 = 4

# Resource types but with names
RESOURCE_CEMENTIA = "Cementia"
RESOURCE_PLASMATIA = "Plasmatia"
RESOURCE_AUFERIA = "Auferia"
RESOURCE_BUFF = "AoE Buff"


