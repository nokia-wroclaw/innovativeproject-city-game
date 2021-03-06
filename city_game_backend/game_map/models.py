from django.db import models
from .utils import round_down
import city_game_backend.CONSTANTS as CONSTANTS
import random

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

    owner_guild = models.ForeignKey('guild_manager.Guild', on_delete=models.SET_NULL, default=None, null=True,
                                    blank=True)

    def __str__(self):
        return f"Chunk: {self.latitude_lower_bound}, {self.longitude_lower_bound}"

    def fill_with_random_structures(self, how_many):
        if how_many > 10:
            how_many = 10

        latitudes = [x / 10 * CONSTANTS.CHUNK_SIZE + self.latitude_lower_bound
                     for x in random.sample(range(0, 10), how_many)]

        longitudes = [x / 10 * CONSTANTS.CHUNK_SIZE + self.longitude_lower_bound
                      for x in random.sample(range(0, 10), how_many)]

        for struct_lon, struct_lat in zip(longitudes, latitudes):
            new_struct = Structure()
            new_struct.longitude = struct_lon
            new_struct.latitude = struct_lat
            new_struct.tier = random.randint(1, 3)
            new_struct.resource_type = random.randint(1, 2)
            new_struct.resources_left = new_struct.tier * 100
            new_struct.save()

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
    rotation = models.FloatField(default=0)  # Directly related to the Unity's Y rotation

    chunk = models.ForeignKey(Chunk, on_delete=models.CASCADE)

    # Gameplay-specific data
    taken_over = models.BooleanField(default=False)
    owner = models.ForeignKey('player_manager.Player', on_delete=models.CASCADE, default=None, null=True, blank=True)

    # Guild-specific data
    taken_over_by_guild = models.BooleanField(default=False)
    owner_guild = models.ForeignKey('guild_manager.Guild', on_delete=models.CASCADE, default=None, null=True, blank=True)

    tier = models.IntegerField(default=1)

    # Resources types
    # # We'll think about names later
    GAME_RESOURCES = (
        (CONSTANTS.RESOURCE_TYPE_1, CONSTANTS.RESOURCE_CEMENTIA),
        (CONSTANTS.RESOURCE_TYPE_2, CONSTANTS.RESOURCE_PLASMATIA),
        (CONSTANTS.RESOURCE_TYPE_3, CONSTANTS.RESOURCE_AUFERIA),
        (CONSTANTS.RESOURCE_TYPE_4, CONSTANTS.RESOURCE_BUFF)
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
