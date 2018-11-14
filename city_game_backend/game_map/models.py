from django.db import models
import math
import city_game_backend.CONSTANTS as CONSTANTS


def round_down(n):
    return math.floor(n * 100) / 100


class Chunk(models.Model):
    """
    A map is divided into chunks - each is 0.01 x 0.01 degrees latitude and longitude,
    that makes a single chunk around 111x111 meters in size
    """

    latitude_lower_bound = models.FloatField()
    longitude_lower_bound = models.FloatField()

    latitude_upper_bound = models.FloatField()
    longitude_upper_bound = models.FloatField()

    # All the roads inside a json
    roads = models.TextField()

    def __str__(self):
        return f"Chunk: {self.latitude_lower_bound}, {self.longitude_lower_bound}"


class Structure(models.Model):
    """
    Every structure visible to other players that is, in some way, interactive
    * Mineral sources
    * Mineral mines, built by the players
    * Other player-built structures
    """

    # Location data
    latitude = models.FloatField()
    longitude = models.FloatField()
    chunk = models.ForeignKey(Chunk, on_delete=models.CASCADE)

    # Gameplay-specific data
    taken_over = models.BooleanField(default=False)
    owner = models.ForeignKey('player_manager.Player', on_delete=models.CASCADE, default=None, null=True, blank=True)
    tier = models.IntegerField(default=1)

    # Resources types
    # # We'll think about names later
    GAME_RESOURCES = (
        (CONSTANTS.RESOURCE_TYPE_1, 'Resource 1'),
        (CONSTANTS.RESOURCE_TYPE_2, 'Resource 2'),
        (CONSTANTS.RESOURCE_TYPE_3, 'Resource 3'),
    )

    resource_type = models.IntegerField(choices=GAME_RESOURCES)
    resources_left = models.FloatField(default=100)  # TODO: think on the default values

    # Automatic setting of the chunk field
    def save(self, *args, **kwargs):
        chunk_longitude = round_down(self.longitude)
        chunk_latitude = round_down(self.latitude)

        on_which_chunk: Chunk = Chunk.objects.filter(
            longitude_lower_bound=chunk_longitude,
            latitude_lower_bound=chunk_latitude
        ).first()

        if on_which_chunk is None:
            raise Exception("No chunk to place the Structure on!")

        self.chunk = on_which_chunk

        super(Structure, self).save(*args, **kwargs)
