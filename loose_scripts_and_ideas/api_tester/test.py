import gi
gi.require_version('Gtk', '3.0')
from gi.repository import Gtk 

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
        Gtk.Window.__init__(self, title="Hello World")

        self.two_panes_layout_box = Gtk.Box(spacing=60)
        self.add(self.two_panes_layout_box)

        self.left_panel_grid = Gtk.Grid()
        self.log_panel = Gtk.TextView()

        self.two_panes_layout_box.pack_start(self.left_panel_grid, True ,True , 0)
        self.two_panes_layout_box.pack_start(self.log_panel, True ,True , 0)

        #self.log_panel.insert_at_cursor("omg")

        self.login_label = Gtk.Label(label="Login")

        self.login_entry = Gtk.Entry()
        self.password_entry = Gtk.Entry()

        button4 = Gtk.Button(label="Button 4")
        button5 = Gtk.Button(label="Button 5")
        button6 = Gtk.Button(label="Button 6")

        self.left_panel_grid.attach(self.login_label, 0, 0, 1, 1)
        self.left_panel_grid.attach_next_to(self.login_entry, self.login_label, Gtk.PositionType.BOTTOM, 1, 1)
        self.left_panel_grid.attach_next_to(self.password_entry, self.login_entry, Gtk.PositionType.BOTTOM, 1, 1)

        self.left_panel_grid.attach_next_to(button4, self.login_entry , Gtk.PositionType.RIGHT, 2, 1)
        self.left_panel_grid.attach(button5, 1, 2, 1, 1)
        self.left_panel_grid.attach_next_to(button6, button5, Gtk.PositionType.RIGHT, 1, 1)

    def on_button1_clicked(self, widget):
        print("Hello")

    def on_button2_clicked(self, widget):
        print("Goodbye")

win = MyWindow()
win.connect("destroy", Gtk.main_quit)
win.show_all()
Gtk.main()
