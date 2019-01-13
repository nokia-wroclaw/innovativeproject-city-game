from .message_utils import SUCCESS_MESSAGE, require_message_content


@require_message_content(
    ('lat', float),
    ('lon', float),
    ('rotation', float),
    ('tier', int)
)
def handle_building_placement_request(message: dict, websocket) -> str:
    return SUCCESS_MESSAGE
