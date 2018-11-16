import json
import math
import functools


SUCCESS_MESSAGE = {'status': 'success'}


def error_message(message):
    return json.dumps({
        'status': 'error',
        'message': message
    })


# Rounds down to chunk coordinates (see CONSTANTS.CHUNK_SIZE)
def round_down(n):
    return math.floor(n * 100) / 100


def require_message_content(*required_arguments):
    """
        Decorator for websocket data filtering:

        Websocket gets the request data via a dictionary.
        This decorator is used to filter the data dictionary
        and check if it contains specific data (in a specific type)

        e.g:

        @require_message_content(
            ('lat', float),
            ('lon', float),
            ('username', str)
        )
        def some_handler(message):
            ... etc

        ^ will check if message dictionary contains:
            - 'lat' type float
            - 'lon' also type float
            - 'username' type string

        if the requirements are not met, the function will return the standard error_message (see functions above)
    """

    def on_no_arg(argument):
        return error_message(f"{argument} not found!")

    def on_wrong_arg_type(argument, is_type, should_be_type):
        return error_message(f"{argument} should be type {should_be_type} but {is_type} was given!")

    def decorator_require_message_content(func):
        @functools.wraps(func)
        def wrapper_require_function_content(*args, **kwargs):
            dict_to_filter = args[0]

            for argument in required_arguments:
                if argument[0] not in dict_to_filter:
                    return on_no_arg(argument[0])

                elif type(dict_to_filter[argument[0]]) is not argument[1]:  # TODO: Less spaghetti
                    return on_wrong_arg_type(argument[0], type(dict_to_filter[argument[0]]), argument[1])

            return func(*args, **kwargs)
        return wrapper_require_function_content
    return decorator_require_message_content

