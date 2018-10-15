from channels.auth import AuthMiddlewareStack
from channels.routing import ProtocolTypeRouter, URLRouter
import websocket_controller.routing

application = ProtocolTypeRouter({
    # (http->django views is added by default)
    'websocket': AuthMiddlewareStack( # TODO: Learn more about the AuthMiddlewareStack
        URLRouter(
            websocket_controller.routing.websocket_urlpatterns
        )
    ),
})