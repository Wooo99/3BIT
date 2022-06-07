from django.urls import path
from . import views

urlpatterns = [
    path("", views.home, name="home"),
    path('join_game', views.join_game),
]