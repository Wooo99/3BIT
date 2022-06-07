from django.shortcuts import render, redirect
from django.contrib.auth import login, authenticate
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm
from ITU_projekt.models import Elo, Match

# Create your views here.
def register(response):
    if response.method == 'POST':
        form = UserCreationForm(response.POST)
        if form.is_valid():
            # create new user
            form.save()
            # create Elo for the user
            username = form.cleaned_data['username']
            user = User.objects.get(username=username)
            userElo = Elo(userName=user, elo=100)
            userElo.save()
            return redirect('/login/')
    else:
        form = UserCreationForm()
    return render(response, 'register.html', {'form': form})