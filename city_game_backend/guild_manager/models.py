from django.db import models


class Guild(models.Model):
    guild_name = models.TextField()

    # Guild's resources
    Cementia = models.FloatField(default=0.0)
    Plasmatia = models.FloatField(default=0.0)
    Auferia = models.FloatField(default=0.0)

