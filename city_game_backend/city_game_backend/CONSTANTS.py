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
MESSAGE_TYPE_GUILD_DATA_REQUEST = 8
MESSAGE_TYPE_MULTIPLAYER_STRUCT_TAKEOVER_REQUEST = 9
MESSAGE_TYPE_SEND_GUILD_INVITE = 10
MESSAGE_TYPE_RESPOND_TO_GUILD_INVITE = 11
MESSAGE_TYPE_SEND_GUILD_KICK_REQUEST = 12
MESSAGE_TYPE_CHUNK_OWNER_REQUEST = 13

# GuildInvite related
GUILD_INVITE_ACCEPT = 1
GUILD_INVITE_DENY = 2

# Server-driven (also referred to as special) message types
SPECIAL_MESSAGE_MAP_UPDATE = 0
SPECIAL_MESSAGE_NOTIFICATION = 1
SPECIAL_MESSAGE_GUILD_MEMBER_POSITION_UPDATE = 2
SPECIAL_MESSAGE_GUILD_INVITE_NOTIFICATION = 3

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

PLAYER_IS_GONE_COORD = 2137


