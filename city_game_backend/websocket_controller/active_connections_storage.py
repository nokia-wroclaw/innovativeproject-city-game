class ActiveConnectionsStorage:
    """
    The class stores every active websocket connection,
    allows to search for a specific connection and will probably provide more helpers in the future
    """
    connections = {}

    @staticmethod
    def clear():
        for connection in ActiveConnectionsStorage.connections:
            connection.close()
        ActiveConnectionsStorage.connections = {}

    @staticmethod
    def get(player_id: int):
        if player_id in ActiveConnectionsStorage.connections:
            return ActiveConnectionsStorage.connections[player_id]
        else:
            return None

    @staticmethod
    def add(player_id: int, connection):
        if player_id not in ActiveConnectionsStorage.connections:
            ActiveConnectionsStorage.connections[player_id] = connection

    @staticmethod
    def remove(client_id: int):
        if client_id in ActiveConnectionsStorage.connections:
            del ActiveConnectionsStorage.connections[client_id]
