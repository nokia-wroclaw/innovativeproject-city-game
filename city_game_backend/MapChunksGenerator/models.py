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

    def __str__(self):
        return f"Chunk: {self.latitude_lower_bound}, {self.longitude_lower_bound}"


class RoadNode(models.Model):
    """
    A Chunk owns many RoadNodes that store the position of roads inside a given chunk
    """
    chunk = models.ForeignKey(Chunk, on_delete=models.CASCADE)

    latitude_start = models.FloatField()
    latitude_end = models.FloatField()

    longitude_start = models.FloatField()
    longitude_end = models.FloatField()