from django.db import models
from django.contrib.auth.models import User
from datetime import datetime

class newMatch(models.Model):
    joinCode = models.IntegerField(primary_key=True)
    begin = models.BooleanField(default=False)
    player1 = models.ForeignKey(User, null=False, on_delete=models.CASCADE, related_name='newmatch_player1_set')
    player2 = models.ForeignKey(User, null=True, on_delete=models.CASCADE, related_name='newmatch_player2_set')
    winner1 = models.CharField(null=True, max_length=50)
    winner2 = models.CharField(null=True, max_length=50)


class Match(models.Model):
    id = models.IntegerField(primary_key=True)
    date = models.DateTimeField(default=datetime.utcnow, blank=True)
    player1 = models.ForeignKey(User, null=True, on_delete=models.CASCADE, related_name='player1_set')
    player2 = models.ForeignKey(User, null=True, on_delete=models.CASCADE, related_name='player2_set')
    winner = models.ForeignKey(User, null=True, on_delete=models.CASCADE, related_name='winner_set')


class Elo(models.Model):
    userName = models.OneToOneField(User, on_delete=models.CASCADE, primary_key=True)
    elo = models.IntegerField()
