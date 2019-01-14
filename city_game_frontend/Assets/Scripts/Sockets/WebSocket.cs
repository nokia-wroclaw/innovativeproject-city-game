using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sockets
{
    
    /*
     * Wraps around every single websocket request
     */
    public class Request
    {

        /*
         * Since the messages are received inside the Websocket object, which works asynchronously, 
         * they cannot use the Unity's functions and classes (game object spawning, modyfying them, etc)
         * 
         * This is why the callbacks will be passed to the GameManager's event queue,
         * and then processed synchronically within the GameManager's Update function.
         * 
         * Since the sender object reference won't be available in the callback function (no this.transform etc, since this becomes something else)
         * we will pass the sender's reference to be able to use its methods in the callbacks
         * 
         */
        public delegate void callbackFunc(GameObject sender, string error, string data);
        public callbackFunc callback; 

        public RequestData requestData;
        private ResponseData responseData = null;

        public void setReponseData(ResponseData receivedData)
        {
            this.responseData = receivedData;
        }

        public ResponseData getResponseData()
        {
            return this.responseData;
        }

        /*
         * This will ONLY be called from the game manager AFTER the websocket has written the response data into the Request object
         * via the setResponseData method
         */
        public void performCallback()
        {
            try { 
                this.callback(
                    this.requestData.getSender(),
                    this.responseData.error,
                    this.responseData.message
                );
            } catch(Exception e)
            {
                Debug.LogError("ERROR IN HANDLING");
                Debug.LogError(this.responseData.message);
                Debug.LogError(e.Data);
                Debug.LogError(e.StackTrace);
            }
        }

        public Request()
        {
            requestData = new RequestData();
        }
    }


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
    public class RequestData
    {
        public string data; //data that will be send to the server
        public int id; //transaction id

        // Is private so it won't be serialized
        private GameObject sender;

        // TODO: Check the c# properties instead of using standard setters/getters
        public void setSender(GameObject sender)
        {
            this.sender = sender;
        }

        public GameObject getSender()
        {
            return this.sender;
        }
    }

    public class ResponseData
    {
        public string error;
        public string message;

        // The two below only apply if the message is a special, not used in the standard request -> callback mechanism
        public bool isSpecialMessage;
        public int specialMessageID;

        public ResponseData(string error, string message)
        {
            this.error = error;
            this.message = message;

            isSpecialMessage = false;
        }
    }

    public class WebSocket
    {
        public GameManager gameManager;


        /*
         * each request gets a transaction ID, which will be incremented during every single request
         * this way we can find the right callback function for each request, when the reponse comes
         */
        private int current_transaction_number = 101; 



        private WebSocketSharp.WebSocket socket;
        private string url = "";

        private List<Request> sentData = new List<Request>(); //!< send data and callbacks

        
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

      
        public WebSocket(string url)
        {

            //is socket connected end function
            if (this.isConnected == true) return;

            socket = new WebSocketSharp.WebSocket("ws://" + url);
            socket.Connect();
            this.url = url;

           //on received event
            socket.OnMessage += (sender, e) =>
            {
    
                //parse data to get transaction id
                var receivedMessage = JsonUtility.FromJson<RawReceivedMessage>(e.Data);

                /* 
                 * Special messages - the ones that are not client requests, but a info pushed from the server without asking for it
                 * such as notifications and map structures changes have ids smaller than 100. They are treated differently
                 */
                if (receivedMessage.id < 100)
                {
                    Request newSpecialRequest = new Request();


                    ResponseData specialResponseData = new ResponseData("", receivedMessage.message);
                    specialResponseData.isSpecialMessage = true;
                    specialResponseData.specialMessageID = receivedMessage.id;

                    // Need to fake some RequestData, it's not used but the callbacks throw exception if there is no data sent
                    // TODO: RETHINK
                    RequestData specialRequestData = new RequestData();
                    specialRequestData.setSender(null);


                    newSpecialRequest.setReponseData(
                        specialResponseData
                    );

                    GameManager.callbacksToProcess.Enqueue(newSpecialRequest);

                    return;
                }
                 
                Request request = sentData.Find(r => r.requestData.id == receivedMessage.id);
                request.setReponseData(
                    new ResponseData(/* TODO: Add error handling and put it there -> */ "", receivedMessage.message)
                );
                GameManager.callbacksToProcess.Enqueue(request);
                
                sentData.Remove(request);
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
         * Send a new request
         */
        public void send(GameObject sender, string messageToSend, Request.callbackFunc callback)
        {
            //Debug.Log("Socket " + url + " received a send order: " + messageToSend);

            
            //TODO: DESPAGHETTIZE
            // create a Request constructor
            Request request = new Request();
            request.callback = callback;

            request.requestData.setSender(sender);


            request.requestData.data = messageToSend;
            request.requestData.id = this.current_transaction_number++; //transaction id


            // huh?
            if (sentData == null)
                Debug.Log("Senddata is null!");

            sentData.Add(request);

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