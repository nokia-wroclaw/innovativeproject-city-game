from player_manager.models import ActivePlayer
from websocket_controller.active_connections_storage import ActiveConnectionsStorage
from city_game_backend import CONSTANTS
from guild_manager.utils import notify_players_about_guild_member_position_change

def handle_disconnect_event(websocket):
    active_player_to_remove: ActivePlayer = ActivePlayer.get_from_player_id(websocket.player_id)

    if active_player_to_remove is not None:
        active_player_to_remove.latitude = CONSTANTS.PLAYER_IS_GONE_COORD
        active_player_to_remove.longitude = CONSTANTS.PLAYER_IS_GONE_COORD
        active_player_to_remove.save()
        notify_players_about_guild_member_position_change(active_player_to_remove)

        active_player_to_remove.delete()

    ActiveConnectionsStorage.remove(websocket.player_id)
