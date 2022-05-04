% Zad�n� �. 17:
% Napi�te program �e��c� �kol dan� predik�tem u17(LIN1,LIN2,VOUT), kde LIN1 a 
% LIN2 jsou vstupn� ��seln� seznamy/vektory se stejn�m po�tem prvk� a VOUT je 
% prom�nn�, ve kter� se vrac� hodnota skal�rn�ho sou�inu vektor� LIN1 a LIN2. 

% Testovac� predik�ty:                                  	% VOUT
u17_1:- u17([5,-3,2,4,12],[-3,1,0,6,-2],VOUT),write(VOUT).	% -18
u17_2:- u17([],[],VOUT),write(VOUT).                         % 0
u17_3:- u17([5.1],[3.3],VOUT),write(VOUT).                   % 16.83
u17_r:- write('Zadej LIN1: '),read(LIN1),
        write('Zadej LIN2: '),read(LIN2),
        u17(LIN1,LIN2,LOUT),write(LOUT).


u17(LIN1,LIN2,VOUT):- uloha17(LIN1,LIN2,VOUT).
u17([], [], 0).
uloha17([], [], 0).
uloha17([A|REST],[B|REST2],S):- uloha17(REST, REST2, S1), S is S1 + A * B.