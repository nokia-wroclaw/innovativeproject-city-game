from tkinter import *
from websocket import create_connection
import json

SERVER_URL = "ws://localhost:8000/ws/"

def load_chunk():
    w.delete('all')
    lon = float(lon_entry.get())
    lat = float(lat_entry.get())

    data = {
        'type': 'get_map',
        'lon': lon,
        'lat': lat
    }

    ws = create_connection(SERVER_URL)
    ws.send(json.dumps(data))
    print("Sent")
    print("Receiving...")
    result = ws.recv()
    print("Received '%s'" % result)
    ws.close()


master = Tk()

lat_entry = Entry(master)
lat_entry.pack()
lat_entry.insert(0, "LAT")

lon_entry = Entry(master)
lon_entry.pack()
lon_entry.insert(0, "LON")


confirm = Button(master, text='load', command=load_chunk)
confirm.pack()

w = Canvas(master, width=1000, height=1000)


w.pack()

mainloop()