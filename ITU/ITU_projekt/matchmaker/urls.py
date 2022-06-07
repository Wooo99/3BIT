"""ITU_projekt URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/3.2/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path, include
from . import views


urlpatterns = [
    path('new/', views.createLobby),
    path('join/<int:lobbyID>', views.joinLobby),
    path('join/<int:lobbyID>/connected', views.connected),
    path('join/<int:lobbyID>/match_started_joined', views.match_started_joined),
    path('new/connected', views.connected),
    path('new/match_started_new', views.match_started_new),
    path('match_concluded/', views.match_concluded),
    path('match_quit/', views.match_quit),

]
