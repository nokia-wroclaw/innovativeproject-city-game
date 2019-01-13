from player_manager.models import ActivePlayer
from player_manager.models import Player
from .models import Guild
from websocket_controller.active_connections_storage import ActiveConnectionsStorage
import json
from city_game_backend import CONSTANTS


def notify_players_about_guild_member_position_change(activePlayer: ActivePlayer):
    player: Player = activePlayer.player
    guild = player.guild

    if guild is None:
        return

    players_to_notify = ActivePlayer.objects.filter(
        player__guild=guild
    )

    message = json.dumps({
        'id': CONSTANTS.SPECIAL_MESSAGE_GUILD_MEMBER_POSITION_UPDATE,
        'message': json.dumps({
            'id': activePlayer.player.pk,
            'lon': activePlayer.longitude,
            'lat': activePlayer.latitude,
            'rotation': activePlayer.rotation,
            'nick': activePlayer.player.nickname
        })
    })

    # Filtering the currently active players in search of guild members
    active_guild_member: ActivePlayer

    for active_guild_member in players_to_notify:
        if active_guild_member.player.pk == player.pk:
            continue

        ActiveConnectionsStorage.get(active_guild_member.player.id).send(message)
