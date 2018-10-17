from django.contrib.auth import authenticate
import logging
from .active_connections_storage import ActiveConnectionsStorage
from player_manager.models import Player, ActivePlayer
import json

logger = logging.getLogger(__name__)


def handle_auth_event(message: dict, websocket):
    """
    In order to authenticate, the user must send the following:
    login: username
    pass: user's password

    :param message:
    :param websocket:
    :return:
    """
    username = message['login']
    password = message['pass']

    logging.info(f'received auth request from {username}')
    user = authenticate(username=username, password=password)

    if user is not None:
        logging.info(f'User {username} authenticated!')

        # Getting the player object of the user who has just logged in
        player = Player.objects.filter(
            user=user
        ).first()

        # Checking if the account has a user object (shouldn't happen, but better make sure)
        if player is None:
            logger.critical(f'PLAYER ACCOUNT CONFIGURED WRONGLY, USERNAME: {user.username}')
            websocket.close()
            return

        # Checking if the player is not logged in already
        check_if_logged_in = ActivePlayer.objects.filter(
            player=player
        ).first()

        if check_if_logged_in is not None:
            logger.info(f'{user.username} tried to log in twice!')
            websocket.close()
            return

        '''
        After making sure that:
        * The credentials given by the player were correct
        * The player account is correctly configured
        * The player is not currently logged in
        
        We will add the player to the ActivePlayers table and save this Websocket connection
        '''

        # Adding the player into the ActivePlayers table
        new_active_player = ActivePlayer(
            player=player
        )

        new_active_player.save()

        # Saving the websocket connection
        websocket.user = user
        websocket.player = player
        ActiveConnectionsStorage.add(user.id, websocket)
        websocket.send(
            json.dumps({'auth_event': 'success'})  # TODO: think about where to put those success/failure messages
        )
    else:
        websocket.close()
