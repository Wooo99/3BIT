from django import forms


class MatchWinForm(forms.Form):
    choice = forms.CharField(label='Who won?')

    def __init__(self, player1, player2, enable):
        super(MatchWinForm, self).__init__()
        self.fields['choice'].widget = forms.RadioSelect(choices=[('player1', player1), ('player2', player2)])
        self.fields['choice'].disabled = enable
