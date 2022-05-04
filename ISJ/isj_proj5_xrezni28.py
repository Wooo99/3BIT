#!/usr/bin/env python3


def gen_quiz(qpool, *index, altcodes=('A', 'B', 'C', 'D', 'E', 'F'), quiz=None):
    
    if quiz is None:
        quiz = []

    if not index:
        return quiz

    else:

        qpool_list = []
        for i in index:
            try:
                qpool_list.append(qpool[int(i)])
            except IndexError as error:
                print("Ignoring index %d - %s" % (int(i), error))

    for question, answers in qpool_list:

        zipped_answers_list = []

        [zipped_answers_list.append("%s: %s" % (altcode, answer)) for altcode, answer in zip(altcodes, answers)]

        quiz.append((question, zipped_answers_list))
    return quiz


if __name__ == "__main__":
    import doctest
    doctest.testmod()