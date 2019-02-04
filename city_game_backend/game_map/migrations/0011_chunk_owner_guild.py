# Generated by Django 2.1.2 on 2019-02-04 08:59

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('guild_manager', '0002_guildinvite'),
        ('game_map', '0010_auto_20190113_1106'),
    ]

    operations = [
        migrations.AddField(
            model_name='chunk',
            name='owner_guild',
            field=models.ForeignKey(blank=True, default=None, null=True, on_delete=django.db.models.deletion.SET_NULL, to='guild_manager.Guild'),
        ),
    ]
