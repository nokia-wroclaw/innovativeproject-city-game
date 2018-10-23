using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System;


namespace Assets.Sockets
{
    public class WebSocket
    {
        private WebSocketSharp.WebSocket socket;
        private string url = "";

        private List<string> received; //!< received data 
        private List<string> toSend; //!< to send data 

        public bool loggedIn { set; get; } //!< is player logged in server

        /**
         * Get connected status. if true then connection is established and
         * data transmision is possible
         */
        public bool isConnected
        {
            get
            {
                if (socket == null) return false;

                //return true if communiacte is possible
                return (socket.ReadyState == WebSocketSharp.WebSocketState.Open);
            }
        }

        /**
         * check if there is some new data
         */
        public bool isData { get
            {
                return received.Count > 0;
            }
        }

        public bool busy = false; //!< true if socket is waiting for respons from server

        public WebSocket()
        {
            toSend = new List<string>();
            received = new List<string>();

        }

        public void connect(string url)
        {
            //is socket connected end function
            if (this.isConnected == true) return;

            socket = new WebSocketSharp.WebSocket("ws://" + url);
            socket.Connect();
            this.url = url;

            //clear toSend before next send
            toSend.Clear();


            //on received event
            socket.OnMessage += (sender, e) =>
            {
                Debug.Log("Socket " + this.url + " received a data: " + e.Data);
                busy = false; //socket is ready to send and receive next data
                received.Add(e.Data); //add data to received list
            };

            //on open event
            socket.OnOpen += (sender, e) =>
            {
                Debug.Log("Socket " + this.url + " has been connected");
            };

            //on received event
            socket.OnError += (sender, e) =>
            {
                Debug.Log("Socket " + this.url + " received an error: " + e.Message);

            };
        }

        /**
         * Send function add new frame to sending list
         */ 
        public void send(string data)
        { 
            Debug.Log("Socket " + url + " received a send order: " + data);
            toSend.Add(data);
        }

        /**
         * Specialized function to send Login request
         */
        public void sendLogReq(string user, string pass)
        {
            string req = "{\"login\":\"" + user + "\",\"pass\": \"" + pass + "\", \"type\":\"auth_event\"}";
            send(req);
        }

        /**
         * Specialized function to send chunk request
         */
        public void sendChunkReq(double longitute, double latitude)
        {
            string req = "{\"lat\":\"" + latitude + "\",\"lon\": \"" + longitute+ "\", \"type\":\"location_event\"}";
            send(req);
        }

        /**
         * Disconnect from the server
         */
        public void disconnect()
        {
            socket.Close(); //close the connection with status 1005
            socket = null;
        }


        


        /**
         * Function check if there is something to send, and send it.
         * It could be run in subroutine
         */
        public void processOrders()
        {
            if (toSend.Count <= 0 || //no data to send
                isConnected() == false || //isn't connected
                busy == true) //waiting for response
                return;
            
            //send data
            socket.Send(toSend.ElementAt(0));

            //remove it from the list
            toSend.RemoveAt(0);
            busy = true; //socket has just send data. Waiting for response
            Debug.Log("Socket " + url + " send data. last: " + toSend.Count);
        }

        public string getData()
        {
            string buf = received.ElementAt(0);
            received.RemoveAt(0);
            return buf;
        }
    }
}
