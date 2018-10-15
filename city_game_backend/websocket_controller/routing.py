from django.conf.urls import url
from websocket_controller import consumers

"""
JUST THE ROUTING FOR THE WEBSOCKETS

That's how the Channels library works ;)
"""

websocket_urlpatterns = [
    url(r'^ws/$', consumers.ChatConsumer),
]
