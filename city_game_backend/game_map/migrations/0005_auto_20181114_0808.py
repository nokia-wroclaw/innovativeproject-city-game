# Generated by Django 2.1.2 on 2018-11-14 08:08

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('game_map', '0004_structure'),
    ]

    operations = [
        migrations.AlterField(
            model_name='structure',
            name='owner',
            field=models.ForeignKey(default=None, null=True, on_delete=django.db.models.deletion.CASCADE, to='player_manager.Player'),
        ),
    ]
