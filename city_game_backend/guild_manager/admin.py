from django.contrib import admin

# Register your models here.

from .models import Guild, GuildInvite

admin.site.register(Guild)
admin.site.register(GuildInvite)
