from django.conf.urls import url
from . import consumers

"""
JUST THE ROUTING FOR THE WEBSOCKETS

That's how the Channels library works ;)
"""

websocket_urlpatterns = [
    url(r'^ws/$', consumers.ChatConsumer),
]
