# Generated by Django 2.1.2 on 2018-10-15 16:31

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Chunk',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('latitude_lower_bound', models.FloatField()),
                ('longitude_lower_bound', models.FloatField()),
                ('latitude_upper_bound', models.FloatField()),
                ('longitude_upper_bound', models.FloatField()),
            ],
        ),
        migrations.CreateModel(
            name='RoadNode',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('latitude_start', models.FloatField()),
                ('latitude_end', models.FloatField()),
                ('longitude_start', models.FloatField()),
                ('longitude_end', models.FloatField()),
                ('chunk', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='game_map.Chunk')),
            ],
        ),
    ]