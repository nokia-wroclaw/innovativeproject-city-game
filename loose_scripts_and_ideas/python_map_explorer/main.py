from tkinter import *
from websocket import create_connection
import json

SERVER_URL = "ws://localhost:8000/ws/"
SCALE = 20000


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
    #print("Received '%s'" % result)
    ws.close()
    print('Done receiving')
    message = json.loads(result)['get_map']
    message = json.loads(message)
    for model in message:
        node = model['fields']
        lat_start = node['latitude_start'] - lat
        lat_end = node['latitude_end'] - lat

        lon_start = node['longitude_start'] - lon
        lon_end = node['longitude_end'] - lon

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
lon_entry.insert(0, "17.04")


confirm = Button(master, text='load', command=load_chunk)
confirm.pack()

w = Canvas(master, width=1000, height=1000)
w.configure(background='#263238')

w.pack()

mainloop()