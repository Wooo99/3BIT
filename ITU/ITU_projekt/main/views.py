from django.http import JsonResponse
from django.shortcuts import render
from ITU_projekt.models import User, Elo, Match, newMatch


def home(response):
    # get the logged in user
    username = None
    top10 = None
    userElo = 0
    matches = None
    if response.user.is_authenticated:
        # get the users elo
        userElo = Elo.objects.get(userName=response.user).elo
        # get top 10
        top10 = Elo.objects.order_by('-elo')[:10]
        matches = Match.objects.order_by('-id')[:8]
    return render(response, 'main/main.html', {'elo': userElo, 'leaders': top10, 'concl_displ': False,
                                               'quit_displ': False, 'matches': matches})


def join_game(response):
    if response.is_ajax():
        entered_pin = response.POST.get('pin')
        print(entered_pin)
        # check if the entered pin is correct
        for match in newMatch.objects.all():
            if int(match.joinCode) == int(entered_pin):
                print(match.joinCode)
                return JsonResponse({'pin': match.joinCode})

        return JsonResponse({'pin': None})

    return render(response, 'main/join_game.html', {})

