from django.contrib import admin
from .models import Match, Elo, newMatch

#This lets us edit the DB from admin dashboard
admin.site.register(Match)
admin.site.register(Elo)
admin.site.register(newMatch)