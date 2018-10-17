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
    def get(client_id: int):
        if client_id in ActiveConnectionsStorage.connections:
            return ActiveConnectionsStorage.connections[client_id]
        else:
            return None

    @staticmethod
    def add(client_id: int, connection):
        if client_id not in ActiveConnectionsStorage.connections:
            ActiveConnectionsStorage.connections[client_id] = connection

    @staticmethod
    def remove(client_id: int):
        if client_id in ActiveConnectionsStorage.connections:
            del ActiveConnectionsStorage.connections[client_id]