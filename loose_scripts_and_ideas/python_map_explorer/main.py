from tkinter import *
from websocket import create_connection
import json

SERVER_URL = "ws://localhost:8000/ws/"
SCALE = 40000
CHUNK_SIZE = 0.01


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
    print('Done receiving')
    message = json.loads(result)
    for node in message['road_nodes']:
        print(node)
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

        #print(lat_start, lat_end, lon_start, lon_end)




master = Tk()
master.configure(background='#263238')

lat_entry = Entry(master)
lat_entry.pack()
lat_entry.insert(0, "51.1")

lon_entry = Entry(master)
lon_entry.pack()
lon_entry.insert(0, "17.09")


confirm = Button(master, text='load', command=load_chunk)
confirm.pack()

w = Canvas(master, width=1000, height=1000)
w.configure(background='#263238')

w.pack()

mainloop()