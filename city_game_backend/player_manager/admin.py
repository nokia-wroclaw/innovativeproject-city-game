from django.contrib import admin

# Register your models here.
from .models import Player, ActivePlayer

admin.site.register(Player)
admin.site.register(ActivePlayer)
