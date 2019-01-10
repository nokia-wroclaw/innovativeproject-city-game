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
            Guild.remove_player_from_guild(player)

        player.guild = self
        player.save()

    @staticmethod
    def remove_player_from_guild(player):
        guild = player.guild

        if guild is not None:
            print('GUILD NOT NONE')
            if guild.members_count-1 == 0:
                guild.delete()
                print('DELETING GUILD')

        player.guild = None
        player.save()
