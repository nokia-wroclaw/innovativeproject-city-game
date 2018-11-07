using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sockets
{
    /*
     * Every message from the server is a JSON string containing the following elements:
     * - Transaction id ('id' key) - used to determine the callback function that the message is adressed to
     * - The response message ('message' key) - this is the actual message transferred to the callback function
     */
    [System.Serializable]
    class RawReceivedMessage
    {
        public int id;
        public string message;
    }

    [System.Serializable]
    class RequestData
    {
        public string data; //data that will be send to the server
        public int id; //transaction id
    }

    public class WebSocket
    {

        public delegate void receiveFunc(string error, string data);
        public delegate void callbackFunc(string error, string data); //type of callback funcion

        private int current_transaction_number = 0; // each request gets a transaction ID, which will be incremented during every single request
                                                    // this way we can find the right callback function for each request, when the reponse comes



        /**
         * Private class contains data for one request
         */
        private class Request
        {
            public callbackFunc callback; //receive data callback
            public RequestData requestData;

            public Request()
            {
                requestData = new RequestData();
            }
        }



        private WebSocketSharp.WebSocket socket;
        private string url = "";

        private List<Request> sendData = new List<Request>(); //!< send data and callbacks

        public receiveFunc received { set; private get; }

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
        /*public bool isData { get
            {
                //return received.Count > 0;
            }
        }*/

        //public bool busy = false; //!< true if socket is waiting for respons from server

        public WebSocket()
        {
            //toSend = new List<string>();
            //received = new List<string>();

            received = new receiveFunc((string error, string data) =>
            {
                Debug.Log("received funcion is not implemented");
            });
        }

        public void connect(string url)
        {
            //is socket connected end function
            if (this.isConnected == true) return;

            socket = new WebSocketSharp.WebSocket("ws://" + url);
            socket.Connect();
            this.url = url;

            //clear toSend before next send
            //toSend.Clear();


            //on received event
            socket.OnMessage += (sender, e) =>
            {
                //Debug.Log("Socket " + this.url + " received a data: " + e.Data);
                //busy = false; //socket is ready to send and receive next data
                //received.Add(e.Data); //add data to received list

                //parse data to get transaction id
                var receivedMessage = JsonUtility.FromJson<RawReceivedMessage>(e.Data);
                foreach (Request d in sendData)
                {
                    //finde right callback
                    if (d.requestData.id == receivedMessage.id)
                    {
                        //callback when server responsed
                        d.callback("", receivedMessage.message);
                        return;
                    }
                }

                //server push data to client
                // what is this can somebody explain
                this.received("", e.Data);

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
                Debug.Log(e.Exception);

            };
        }


        /**
         * Send function add new frame to sending list
         */
        public void send(string messageToSend, callbackFunc callback)
        {
            Debug.Log("Socket " + url + " received a send order: " + messageToSend);

            //process data.

            Request request = new Request();
            request.callback = callback;
            request.requestData.data = messageToSend;
            request.requestData.id = this.current_transaction_number++; //transaction id

            if (sendData == null)
                Debug.Log("Senddata is null!");

            sendData.Add(request);

            socket.Send(JsonUtility.ToJson(request.requestData)); //send 
        }


        /**
         * Disconnect from the server
         */
        public void disconnect()
        {
            socket.Close(); //close the connection with status 1005
            socket = null;
        }


    }
}