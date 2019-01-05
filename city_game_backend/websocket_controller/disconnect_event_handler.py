from player_manager.models import ActivePlayer
from websocket_controller.active_connections_storage import ActiveConnectionsStorage


def handle_disconnect_event(websocket):
    active_player_to_remove: ActivePlayer = ActivePlayer.get_from_player_id(websocket.player_id)

    if active_player_to_remove is not None:
        active_player_to_remove.delete()

    ActiveConnectionsStorage.remove(websocket.player_id)
