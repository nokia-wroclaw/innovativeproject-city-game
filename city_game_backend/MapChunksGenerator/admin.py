from django.contrib import admin

# Register your models here.

from .models import Chunk, RoadNode

admin.site.register(Chunk)
admin.site.register(RoadNode)
