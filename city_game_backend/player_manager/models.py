from django.db import models
from django.contrib.auth.models import User

from game_map.models import Chunk
# Create your models here.


class Player(models.Model):
    """
    Player model stores just the data connected with the game mechanics,
    every Player has one User account
    """
    nickname = models.CharField(max_length=20)
    account = models.OneToOneField(
        User,
        on_delete=models.CASCADE,
        null=False,
        blank=False
    )


class ActivePlayer(models.Model):
    """
    Each player that is currently logged in the game and his temporary data - position, chunk number
    """

    account = models.OneToOneField(
        Player,
        null=False,
        blank=False,
        on_delete=models.CASCADE
    )

    latitude = models.FloatField()
    longitude = models.FloatField()

    on_which_chunk = models.ForeignKey(
        Chunk,
        on_delete=models.CASCADE
    )