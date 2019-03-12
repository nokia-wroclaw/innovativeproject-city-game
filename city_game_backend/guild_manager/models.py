from django.db import models


class Guild(models.Model):
    guild_name = models.TextField()

    # Guild's resources
    Cementia = models.FloatField(default=0.0)
    Plasmatia = models.FloatField(default=0.0)
    Auferia = models.FloatField(default=0.0)

    color = models.TextField(default="#C62828")

    CIRCLE = 'c'
    SQUARE = 's'
    TRIANGLE = 't'

    ICONS = (
        (CIRCLE, 'Circle'),
        (SQUARE, 'Square'),
        (TRIANGLE, 'Triangle'),
    )

    icon = models.CharField(
        max_length=1,
        choices=ICONS,
        default=CIRCLE
    )

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


class GuildInvite(models.Model):
    receiver = models.ForeignKey('player_manager.Player', on_delete=models.CASCADE)
    guild = models.ForeignKey(Guild, on_delete=models.CASCADE)

    def __str__(self):
        return f'{self.receiver.nickname}\'s invite to {self.guild.guild_name}'

    @staticmethod
    def get_invites_of_player(player):
        return GuildInvite.objects.filter(
            receiver=player
        ).all()

    def accept(self):
        self.guild.add_player(self.receiver)

    def deny(self):
        self.delete()

    @staticmethod
    def get_by_id(invite_id):
        return GuildInvite.objects.filter(
            id=invite_id
        ).first()
