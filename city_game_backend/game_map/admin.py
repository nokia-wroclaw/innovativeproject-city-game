from django.contrib import admin

# Register your models here.

from .models import Chunk, Structure


def generate_structures(modeladmin, request, queryset):
    failures = 0
    for chunk in queryset:
        try:
            chunk.fill_with_random_structures(5)
        except:
            failures +=1
        print(f'Finished with {failures} failures!')


class ChunkAdmin(admin.ModelAdmin):
    actions = [generate_structures]


admin.site.register(Chunk, ChunkAdmin)
admin.site.register(Structure)
