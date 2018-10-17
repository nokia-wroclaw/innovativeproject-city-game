from tkinter import *
import websocket
try:
    import thread
except ImportError:
    import _thread as thread

import json
import time

SERVER_URL = "ws://localhost:8000/ws/"
SCALE = 20000
CHUNK_SIZE = 0.01

AUTH_EVENT = 'auth_event'
LOCATION_EVENT = 'location_event'

LOGIN_DATA = {
    'type': AUTH_EVENT,
    'login': 'baczek',
    'pass': 'baczekbezraczek'
}


def on_message(ws, message):
    #print(message)

    w.delete('all')

    lon = float(lon_entry.get())
    lat = float(lat_entry.get())

    message = json.loads(message)
    for node in message['road_nodes']:

        lat_start = node['lat_start'] - lat + CHUNK_SIZE
        lat_end = node['lat_end'] - lat + CHUNK_SIZE

        lon_start = node['lon_start'] - lon + CHUNK_SIZE
        lon_end = node['lon_end'] - lon + CHUNK_SIZE


        lat_start *= SCALE
        lat_end *= SCALE
        lon_start *= SCALE
        lon_end *= SCALE

        w.create_line(
            lon_start, lat_start,
            lon_end, lat_end,
            fill='white'
        )




def on_error(ws, error):
    print(error)


def on_close(ws):
    print("### closed ###")


def on_open(ws):
    print('### connection established ###')
    ws.send(json.dumps(LOGIN_DATA))



def send_map_request():
    lon = float(lon_entry.get())
    lat = float(lat_entry.get())

    data = {
        'type': LOCATION_EVENT,
        'lon': lon,
        'lat': lat
    }

    ws.send(json.dumps(data))


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
lat_entry.insert(0, "51.1")

lon_entry = Entry(master)
lon_entry.pack()
lon_entry.insert(0, "17.09")


confirm = Button(master, text='load', command=send_map_request)
confirm.pack()

w = Canvas(master, width=1000, height=1000)
w.configure(background='#263238')

w.pack()

mainloop()
