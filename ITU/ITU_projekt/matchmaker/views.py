from django.http import JsonResponse
from django.shortcuts import render, redirect
from django.contrib.auth.models import User
import django.core.exceptions
from django.db.models import Q
from ITU_projekt.models import newMatch, Match, Elo
from .forms import MatchWinForm
from django.contrib.auth.decorators import login_required
import random
import json


@login_required
def createLobby(response):
    lobbyID = 0
    lobby = None
    form = None
    accept = 'Start match'
    acceptDisable = 'enabled'
    cancelDisable = 'enabled'
    elo1 = 0
    elo2 = 0
    # try to find if user is in a match already
    try:
        lobby = newMatch.objects.get(Q(player1=response.user) | Q(player2=response.user))
        lobbyID = lobby.joinCode
        # if he is yeet him
        if lobby.player2 == response.user:
            return redirect('/match/join/' + str(lobbyID))
    except django.core.exceptions.ObjectDoesNotExist:
        # else make him a new match
        lobbyID = random.randint(1000,9999)
        try:
            while newMatch.objects.get(joinCode=lobbyID):
                lobbyID = random.randint(1000,9999)
        except django.core.exceptions.ObjectDoesNotExist:
            lobby = newMatch(joinCode=lobbyID, player1=response.user)
    lobby.save()

    # user has clicked a button
    if response.method == 'POST':
        print(response.POST)
        if response.POST.get('btnradio'):
            lobby.winner1 = response.POST['btnradio']
            lobby.save()
            commitMatch(lobby)
            return redirect('/match/match_concluded/')
        if response.POST.get('accept'):
            lobby.begin = True
            lobby.save()
        if response.POST.get('cancel'):
            lobby.delete()
            return redirect('/match/match_concluded/')

    if lobby.player2 is None:
        acceptDisable = 'disabled'
        accept = 'Waiting for player to join'

    # try to get user elo
    try:
        elo1 = Elo.objects.get(userName=lobby.player1).elo
        elo2 = Elo.objects.get(userName=lobby.player2).elo
    except django.core.exceptions.ObjectDoesNotExist:
        pass
    if lobby.begin:
        acceptDisable = 'enabled'
        cancelDisable = 'disabled'
        accept = 'Confirm result'
        return render(response, 'main/match.html', {'code': lobbyID, 'player1': lobby.player1, 'elo1': elo1, 'player2': lobby.player2, 'elo2': elo2, 'acceptDisable': acceptDisable, 'accept': accept, 'cancelDisable': cancelDisable})
    else:
        return render(response, 'main/create_match.html', {'code': lobbyID, 'player1': lobby.player1, 'elo1': elo1, 'player2': lobby.player2, 'elo2': elo2,'acceptDisable': acceptDisable, 'accept': accept, 'cancelDisable': cancelDisable})

@login_required
def joinLobby(response, lobbyID):
    lobby = None
    form = None
    accept = 'Waiting for host to start'
    acceptDisable = 'disabled'
    cancelDisable = 'enabled'
    elo1 = 0
    elo2 = 0
    try:
        # try to find if user is in a match already
        lobby = newMatch.objects.get(Q(player1=response.user) | Q(player2=response.user))
        # if he is a leader of a match yeet him
        if lobby.player1 == response.user:
            return redirect('/match/new')
        # if it is not this match then yeet him
        if lobby.joinCode != lobbyID:
            return redirect('/match/join/' + str(lobby.joinCode))
    except django.core.exceptions.ObjectDoesNotExist:
        # user is not in a game, join lobbyID game
        try:
            lobby = newMatch.objects.get(joinCode=lobbyID)
            lobby.player2 = response.user
            lobby.save()
        except django.core.exceptions.ObjectDoesNotExist:
            return redirect('/match/match_concluded/')
    # user has clicked a button
    if response.method == 'POST':
        if response.POST.get('choice'):
            lobby.winner2 = response.POST['choice']
            lobby.save()
            commitMatch(lobby)
            return redirect('/match/match_concluded/')
        if response.POST.get('cancel'):
            lobby.player2 = None
            lobby.save()
            return redirect('/match/match_quit/')

    if lobby.begin:
        cancelDisable = 'disabled'
        acceptDisable = 'enabled'
        accept = 'Confirm result'

    # try to get user elo
    try:
        elo1 = Elo.objects.get(userName=lobby.player1).elo
        elo2 = Elo.objects.get(userName=lobby.player2).elo
    except django.core.exceptions.ObjectDoesNotExist:
        pass

    return render(response, 'main/match.html', {'code': lobbyID, 'player1': lobby.player1, 'elo1': elo1, 'player2': lobby.player2, 'elo2': elo2,'acceptDisable': acceptDisable, 'accept': accept, 'cancelDisable': cancelDisable})


def commitMatch(lobby):
    if lobby.winner1 == '' or lobby.winner2 == '':
        return
    if lobby.winner1 == lobby.winner2:
        # calculate ELO
        k = 32
        elo1 = Elo.objects.get(userName=lobby.player1)
        elo2 = Elo.objects.get(userName=lobby.player2)
        r1 = 10 ** (elo1.elo/400)
        print(r1)
        r2 = 10 ** (elo2.elo/400)
        print(r2)
        e1 = r1 / (r1 + r2)
        print(e1)
        e2 = r2 / (r1 + r2)
        print(e2)
        s1 = 0
        s2 = 0
        if lobby.winner1 == 'player1':
            s1 = 1
            s2 = 0
        else:
            s1 = 0
            s2 = 1
        # update players
        elo1.elo = elo1.elo + k * (s1 - e1)
        elo2.elo = elo2.elo + k * (s2 - e2)
        # commit to db
        elo1.save()
        elo2.save()
        match = Match()
        match.player1 = lobby.player1
        match.player2 = lobby.player2
        if lobby.winner1 == lobby.player1.username:
            match.winner = lobby.player1
        else:
            match.winner = lobby.player2
        match.save()
        # remove lobby
        lobby.delete()

@login_required
def connected(response):
    if response.is_ajax():
        code = response.POST.get('match_id')
        try:
            match = newMatch.objects.get(joinCode=code)
        except django.core.exceptions.ObjectDoesNotExist:
            return JsonResponse({'redirect_me': '/match/match_quit/'})
        if response.POST.get('cancel'):
            match.delete()
            return JsonResponse({'redirect_me': '/match/match_quit/'})
        if match.player2 is not None:
            return JsonResponse({'connected': True,
                                 'player2_name': match.player2.username,
                                 'player2_elo': match.player2.elo.elo,
                                 'redirect_me': None})
        else:
            return JsonResponse({'connected': False, 'redirect_me': None})



# Functions for handling AJAX responses from the server.
# Author: Marek Dohnal

# Handles AJAX requests from the originator od the match.
@login_required
def match_started_new(response):
    if response.is_ajax():
        code = response.POST.get('match_id')
        # In case new match has been deleted, the match is decided,
        # user is redirected to the home page with a prompt
        try:
            match = newMatch.objects.get(joinCode=code)
        except django.core.exceptions.ObjectDoesNotExist:
            return JsonResponse({'match_begin': True, 'redirect_me': '/match/match_concluded/'})
        # Winner was selected using the radio button.
        if response.POST.get('winner'):
            match.winner1 = response.POST.get('winner')
            match.save()
            return JsonResponse({'redirect_me': None})
        # Match begun
        elif response.POST.get('match_id'):
            if match.begin:
                return JsonResponse({'match_begin': True, 'redirect_me': None})
            else:
                return JsonResponse({'match_begin': False, 'redirect_me': None})


# Handles AJAX requests from the opponent, who joined the match.
@login_required
def match_started_joined(response, lobbyID):
    if response.is_ajax():
        match_begin = False
        winner = None
        # In case new match has been deleted, the match is decided,
        # user is redirected to the home page with a prompt
        try:
            match = newMatch.objects.get(joinCode=lobbyID)
        except django.core.exceptions.ObjectDoesNotExist:
            return JsonResponse({'match_begin': False, 'redirect_me': '/match/match_quit/'})
        # The result has been accepted by the opponent using "Confirm button"
        # Conclude the match and redirect me.
        if response.POST.get('accept'):
            match.winner2 = match.winner1
            commitMatch(match)
            return JsonResponse({'match_begin': True, 'winner': None, 'redirect_me': '/match/match_concluded/'})
        if response.POST.get('cancel'):
            match.delete()
            return JsonResponse({'match_begin': False, 'redirect_me': '/match/match_quit/'})
        # The site is periodically asking if the match has begun
        else:
            if match.begin:
                match_begin = True
            else:
                match_begin = False
            # If winner was changed by the match creator, update the opponent.
            if match.winner1 is not None:
                winner = match.winner1
            return JsonResponse({'match_begin': match_begin, 'winner': winner, 'redirect_me': None})


@login_required
def match_quit(response):
    global match_quit_var
    match_quit_var = False
    # Get the logged in user
    username = None
    top10 = None
    userElo = 0
    leaders = ''
    # Retreive the last match
    match = Match.objects.all().last()
    if response.user.is_authenticated:
        # get the users elo
        userElo = Elo.objects.get(userName=response.user).elo
        # get top 10
        top10 = Elo.objects.order_by('-elo')[:10]
        matches = Match.objects.order_by('-id')[:8]
        # Render the page with a shown conclusion popup
        return render(response, 'main/main.html', {'elo': userElo, 'leaders': top10,
                                                   'concl_displ': False, 'quit_displ': True,
                                                   'matches': matches})


# Display the home page with a popup with the result of the last match
@login_required
def match_concluded(response):
    global match_quit_var
    match_quit_var = False
    # Get the logged in user
    username = None
    top10 = None
    userElo = 0
    leaders = ''
    # Retreive the last match
    match = Match.objects.all().last()
    if response.user.is_authenticated:
        # get the users elo
        userElo = Elo.objects.get(userName=response.user).elo
        # get top 10
        top10 = Elo.objects.order_by('-elo')[:10]
        matches = Match.objects.order_by('-id')[:8]
        # Render the page with a shown conclusion popup
        return render(response, 'main/main.html', {'elo': userElo, 'leaders': top10,
                                                   'concl_displ': True, 'quit_displ': False,
                                                   'matches': matches})
