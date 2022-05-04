#!/usr/bin/env python3
import collections

def fn(function):
    return function.__name__ # return name of function

def log_and_count(*args2, **kwargs2):
    def decorator(function):
        def function_inside(*args, **kwargs):
            if "key" in kwargs2:
                kwargs2["counts"][kwargs2["key"]] += 1
            elif args2:
                kwargs2["counts"][list(args2)[0]] += 1
            else:
                kwargs2["counts"][fn(function)] += 1
            print("called {2} with {0} and {1}".format(args,kwargs,fn(function)));
            return function(*args, **kwargs)
        return function_inside  
    return decorator #return decorator
