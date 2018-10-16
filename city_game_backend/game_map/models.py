from django.db import models


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
    road_nodes = models.TextField()

    def __str__(self):
        return f"Chunk: {self.latitude_lower_bound}, {self.longitude_lower_bound}"
