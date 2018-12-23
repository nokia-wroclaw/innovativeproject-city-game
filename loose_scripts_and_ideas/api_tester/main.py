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

def append_to_logs(message):
    logs.insert(INSERT, '>>>>\n')
    logs.insert(INSERT, message + '\n')
    logs.insert(INSERT, '<<<<\n\n')
    logs.see("end")


def on_message(ws, message):
    parsed = json.loads(message)
    append_to_logs(json.dumps(parsed, indent=2))



def on_error(ws, error):
    append_to_logs(error)


def on_close(ws):
    append_to_logs("### closed ###")


def on_open(ws):
    append_to_logs('### connection established ###')


def send_overtake_struct_request():
    try:
        ws.send(create_message({
            'type': MESSAGE_TYPE_OVERTAKE_REQUEST,
            'id': int(overtake_id.get())
        }))
    except websocket._exceptions.WebSocketConnectionClosedException:
        append_to_logs('### CONNECTION IS CLOSED ###')

def send_map_request():
    lon = float(lon_entry.get())
    lat = float(lat_entry.get())

    data = {
        'type': LOCATION_EVENT,
        'lon': lon,
        'lat': lat
    }

    ws.send(create_message(data))


def try_login():
    login_data = {
        'type': AUTH_EVENT,
        'login': login_entry.get(),
        'pass': password_entry.get(),
    }

    ws.send(create_message(login_data))


def coloured_label(root, text):
    return Label(root, text=text, fg='white', bg='#263238')


def separator(root, column):
    coloured_label(root, '            ').grid(column=column, row=1)


ws = websocket.WebSocketApp(
    SERVER_URL,
    on_message = on_message,
    on_error = on_error,
    on_close = on_close)

ws.on_open = on_open

thread.start_new_thread(ws.run_forever, ())


master = Tk()
master.configure(background='#263238')


coloured_label(master, 'Login').grid(row=1, column=1)
login_entry = Entry(master)
login_entry.grid(row=2, column=1)
login_entry.insert(0, "gracz")

separator(master, 2)

password_entry = Entry(master)
password_entry.grid(row=3, column=1)
password_entry.insert(0, "baczekbezraczek")

location = Button(master, text='Login', command=try_login)
location.grid(row=4, column=1)

coloured_label(master, 'Location').grid(row=1, column=3)
lat_entry = Entry(master)
lat_entry.grid(row=2, column=3)
lat_entry.insert(0, "51.23")

lon_entry = Entry(master)
lon_entry.grid(row=3, column=3)
lon_entry.insert(0, "17.10")

location = Button(master, text='Send location', command=send_map_request)
location.grid(row=4, column=3)

separator(master, 4)

coloured_label(master, 'Overtaking').grid(row=1, column=5)
overtake_id = Entry(master)
overtake_id.grid(row=2, column=5)
overtake_id.insert(0, "1")

overtake = Button(master, text='Overtake struct', command=send_overtake_struct_request)
overtake.grid(row=3, column=5)

logs = Text(master, width=150)
logs.grid(column=6, row=6)

mainloop()
