# Generated by Django 2.1.2 on 2018-10-17 15:43

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        ('game_map', '0002_auto_20181016_1856'),
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
    ]

    operations = [
        migrations.CreateModel(
            name='ActivePlayer',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('latitude', models.FloatField()),
                ('longitude', models.FloatField()),
            ],
        ),
        migrations.CreateModel(
            name='Player',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('nickname', models.CharField(max_length=20)),
                ('account', models.OneToOneField(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.AddField(
            model_name='activeplayer',
            name='account',
            field=models.OneToOneField(on_delete=django.db.models.deletion.CASCADE, to='player_manager.Player'),
        ),
        migrations.AddField(
            model_name='activeplayer',
            name='on_which_chunk',
            field=models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='game_map.Chunk'),
        ),
    ]
