from tkinter import *
import websocket
try:
    import thread
except ImportError:
    import _thread as thread

import json
import time

SERVER_URL = "ws://localhost:8000/ws/"
SCALE = 27000
CHUNK_SIZE = 0.01
MARGIN = 0.002

AUTH_EVENT = 0
LOCATION_EVENT = 1
MESSAGE_TYPE_DYNAMIC_CHUNK_DATA_REQUEST = 3
MESSAGE_TYPE_OVERTAKE_REQUEST = 4


def create_message(data):
    transaction = 1

    return json.dumps({
        'id': transaction,
        'data': json.dumps(data)
    })


LOGIN_DATA = {
    'type': AUTH_EVENT,
    'login': 'gracz',
    'pass': 'baczekbezraczek',
}


def on_message(ws, message):
    print(message)

    w.delete('all')

    lon = float(lon_entry.get())
    lat = float(lat_entry.get())

    message = json.loads(message)

    for chunk in message['chunks_data']:
        print('chunk!!')
        for node in chunk['road_nodes']:

            lat_start = node['lat_start'] - lat + CHUNK_SIZE + MARGIN
            lat_end = node['lat_end'] - lat + CHUNK_SIZE + MARGIN

            lon_start = node['lon_start'] - lon + CHUNK_SIZE * 2 + MARGIN * 3
            lon_end = node['lon_end'] - lon + CHUNK_SIZE * 2 + MARGIN * 3

            lat_start = WINDOW_SIZE - lat_start * SCALE
            lat_end = WINDOW_SIZE - lat_end * SCALE
            lon_start = lon_start * SCALE
            lon_end = lon_end * SCALE

            w.create_line(
                lon_start, lat_start,
                lon_end, lat_end,
                fill='white'
            )
            time.sleep(0.001)




def on_error(ws, error):
    print(error)


def on_close(ws):
    print("### closed ###")


def on_open(ws):
    print('### connection established ###')
    ws.send(create_message(LOGIN_DATA))


def send_overtake_struct_request():
    ws.send(create_message({
        'type': MESSAGE_TYPE_OVERTAKE_REQUEST,
        'id': int(overtake_id.get())
    }))


def send_map_request():
    lon = float(lon_entry.get())
    lat = float(lat_entry.get())

    data = {
        'type': LOCATION_EVENT,
        'lon': lon,
        'lat': lat
    }

    ws.send(create_message(data))


ws = websocket.WebSocketApp(
    SERVER_URL,
    on_message = on_message,
    on_error = on_error,
    on_close = on_close)

ws.on_open = on_open

thread.start_new_thread(ws.run_forever, ())


master = Tk()
master.configure(background='#263238')

lat_entry = Entry(master)
lat_entry.pack()
lat_entry.insert(0, "51.23")

lon_entry = Entry(master)
lon_entry.pack()
lon_entry.insert(0, "17.10")


location = Button(master, text='Send location', command=send_map_request)
location.pack()

overtake = Button(master, text='Overtake struct', command=send_overtake_struct_request)
overtake.pack()

overtake_id = Entry(master)
overtake_id.pack()
overtake_id.insert(0, "1")

mainloop()
