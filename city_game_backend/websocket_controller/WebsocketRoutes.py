import logging

logger = logging.getLogger(__name__)


class WebsocketRoutes:
    routes = {}

    @staticmethod
    def route(route: int):

        def decorator(f):

            logger.info(f"Binding {f.__name__} to {route}")
            WebsocketRoutes.routes[route] = f
            return f

        return decorator

    @staticmethod
    def get_route(route: int):
        if route in WebsocketRoutes.routes:
            return WebsocketRoutes.routes[route]

        logger.error(f'Route {route} not implemented! Check the CONSTANTS.py')
        return None
