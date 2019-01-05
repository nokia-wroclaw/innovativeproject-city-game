import gi
gi.require_version('Gtk', '3.0')
from gi.repository import Gtk 

import websocket
try:
    import thread
except ImportError:
    import _thread as thread

import json
import time

from consts import *

'''
        self.button1 = Gtk.Label(label="Hello")
        #self.button1.connect("clicked", self.on_button1_clicked)
        self.box.pack_start(self.button1, True, True, 0)

        self.button2 = Gtk.Label(label="Goodbye")
        #self.button2.connect("clicked", self.on_button2_clicked)
        self.box.pack_start(self.button2, True, True, 0)
'''

class MyWindow(Gtk.Window):

    def __init__(self):
        Gtk.Window.__init__(self, title="Consquare God's Console")

        self.__init_app_skeleton__()
        self.__init_log_panel__()
        self.__init_controls_panel__()

        self.consquare_client = None

    def set_consquare_client(self, client):
        self.consquare_client = client


    def __init_app_skeleton__(self):
        self.two_panes_layout_box = Gtk.Box(spacing=10)
        self.add(self.two_panes_layout_box)

        self.left_panel_grid = Gtk.Grid()
        self.right_panel_log_container = Gtk.ScrolledWindow()

        self.right_panel_log_container.set_hexpand(True)
        self.right_panel_log_container.set_vexpand(True)
        
        self.log_panel = Gtk.TextView()
        self.right_panel_log_container.add(self.log_panel)

        self.two_panes_layout_box.pack_start(self.left_panel_grid, False ,False , 0)
        self.two_panes_layout_box.pack_start(self.right_panel_log_container, True ,True , 0)
        
        self.resize(700, 400)

    def __init_log_panel__(self):
        self.log_buffer = self.log_panel.get_buffer()
        self.log_append('Starting the console...\n')
        #self.log_panel.set_width(5)

    def __init_controls_panel__(self):
        self.__init_login_subpanel__()
        
        button5 = Gtk.Button(label="Send 7 plagues")
        button6 = Gtk.Button(label="b o b o r")

        
        self.left_panel_grid.attach(button5, 1, 2, 1, 1)
        self.left_panel_grid.attach_next_to(button6, button5, Gtk.PositionType.RIGHT, 1, 1)

    def __init_login_subpanel__(self):
        self.login_entry = Gtk.Entry()
        self.login_entry.set_placeholder_text('Login')

        self.password_entry = Gtk.Entry()
        self.password_entry.set_placeholder_text('Password')

        self.login_button = Gtk.Button(label="Log in")
        self.login_button.connect("clicked", self.try_login)

        self.left_panel_grid.attach(self.login_entry, 0, 0, 1, 1)
        self.left_panel_grid.attach_next_to(self.password_entry, self.login_entry, Gtk.PositionType.RIGHT, 1, 1)
        self.left_panel_grid.attach_next_to(self.login_button, self.password_entry , Gtk.PositionType.RIGHT, 1, 1)
        


    def log_append(self, text):
        self.log_buffer.insert(self.log_buffer.get_bounds()[1] ,text=text + '\n')

    def try_login(self, widget):
        login = self.login_entry.get_text()
        password = self.password_entry.get_text()

        self.consquare_client.try_login(login, password)

    def on_button2_clicked(self, widget):
        print("Goodbye")





class ConsquareWebsocketClient():
    def __init__(self, parent_app: MyWindow):
        self.parent_app = parent_app

        self.ws = websocket.WebSocketApp(SERVER_URL,
                              on_message = self.on_message,
                              on_error = self.on_error,
                              on_close = self.on_close)

        self.ws.on_open = self.on_open


    def on_message(self, message):
        message = json.loads(message)#['message']
        #message_parsed = json.loads(message)

        self.parent_app.log_append(
            json.dumps(message, indent=4, sort_keys=False)
        )

    def on_open(self):
        self.parent_app.log_append('## CONNECTION OPENED')
        
    def on_error(self, error):
        self.parent_app.log_append(error)

    def on_close(self):
        self.parent_app.log_append('## CONNECTION CLOSED')

    def run_in_background(self):
        thread.start_new_thread(self.ws.run_forever, ())


    @staticmethod
    def create_message(data):
        transaction = 1

        return json.dumps({
            'id': transaction,
            'data': json.dumps(data)
        })



    def try_login(self, login, password):
        login_data = {
            'type': AUTH_EVENT,
            'login': login,
            'pass': password,
        }

        self.ws.send(self.create_message(login_data))




app = MyWindow()
socket = ConsquareWebsocketClient(app)
app.set_consquare_client(socket)


socket.run_in_background()

app.connect("destroy", Gtk.main_quit)
app.show_all()
Gtk.main()
