from django.db import models


class Guild(models.Model):
    guild_name = models.TextField()

    # Guild's resources
    Cementia = models.FloatField(default=0.0)
    Plasmatia = models.FloatField(default=0.0)
    Auferia = models.FloatField(default=0.0)

    def __str__(self):
        return self.guild_name

    @property
    def members_count(self):
        return len(self.player_set.all())

    def add_player(self, player):

        if player.guild is not None:
            player.guild.remove_player(player)

        player.guild = self
        player.save()

    def remove_player(self, player):
        player.guild = None
        player.save()

        if self.members_count == 0:
            self.delete()
