from player_manager.models import ActivePlayer


def handle_disconnect_event(websocket):
    active_player_to_remove: ActivePlayer = ActivePlayer.objects.filter(
        player=websocket.player
    ).first()

    if active_player_to_remove is not None:
        active_player_to_remove.delete()
