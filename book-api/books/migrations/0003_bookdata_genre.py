# Generated by Django 5.1.1 on 2024-10-22 01:20

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('books', '0002_alter_bookdata_description'),
    ]

    operations = [
        migrations.AddField(
            model_name='bookdata',
            name='genre',
            field=models.CharField(default='fiction', max_length=200),
            preserve_default=False,
        ),
    ]
