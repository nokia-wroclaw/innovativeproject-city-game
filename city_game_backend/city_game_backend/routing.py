from channels.routing import ProtocolTypeRouter
from channels.auth import AuthMiddlewareStack
from channels.routing import ProtocolTypeRouter, URLRouter
import WebsocketController.routing

application = ProtocolTypeRouter({
    # (http->django views is added by default)
    'websocket': AuthMiddlewareStack( # TODO: Learn more about the AuthMiddlewareStack
        URLRouter(
            WebsocketController.routing.websocket_urlpatterns
        )
    ),
})