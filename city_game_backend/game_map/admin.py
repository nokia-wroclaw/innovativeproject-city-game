from django.contrib import admin

# Register your models here.

from .models import Chunk, Structure

admin.site.register(Chunk)
admin.site.register(Structure)
