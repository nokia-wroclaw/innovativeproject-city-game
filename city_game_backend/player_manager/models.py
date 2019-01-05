from django.db import models
from django.contrib.auth.models import User

# Create your models here.


class Player(models.Model):
    """
    Player model stores just the data connected with the game mechanics,
    every Player has one User account
    """
    nickname = models.CharField(max_length=20)
    user = models.OneToOneField(
        User,
        on_delete=models.CASCADE,
        null=False,
        blank=False
    )

    # If the player joins a guild
    guild = models.ForeignKey('guild_manager.Guild', on_delete=models.SET_NULL, null=True, blank=True)

    def __str__(self):
        return f'Player {self.nickname}'

    @staticmethod
    def get_by_id(player_id: int):

        return Player.objects.filter(
            id=player_id
        ).first()


class ActivePlayer(models.Model):
    """
    Each player that is currently logged in the game and his temporary data - position, chunk number
    """

    player = models.OneToOneField(
        Player,
        null=False,
        blank=False,
        on_delete=models.CASCADE
    )

    latitude = models.FloatField(
        null=True
    )
    longitude = models.FloatField(
        null=True
    )

    def __str__(self):
        return f'{self.player.nickname} at {self.longitude}, {self.latitude}'

    @staticmethod
    def get_from_player_id(player_id: int):

        return ActivePlayer.objects.filter(
            player_id=player_id
        ).first()

    @staticmethod
    def get_from_active_player_id(player_id: int):

        return ActivePlayer.objects.filter(
            player_id=player_id
        ).first()
