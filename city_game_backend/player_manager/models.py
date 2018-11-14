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

    def __str__(self):
        return f'Player {self.nickname}'


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

    on_which_chunk = models.ForeignKey(
        'game_map.Chunk',
        on_delete=models.CASCADE,
        null=True
    )

    def __str__(self):
        return f'{self.player.nickname} at {self.longitude}, {self.latitude}'
