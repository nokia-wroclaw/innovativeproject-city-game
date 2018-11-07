using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class ServerSocket : MonoBehaviour {

    private Assets.Sockets.WebSocket socket; //!< socket object

    private bool loggedIn; //!< true if socket is logged into django

    private Assets.TimerSync timer = new Assets.TimerSync();


    public class LoginData
    {
        public int type = 0;
        public string login = "gracz";
        public string pass = "baczekbezraczek";
    }

    [System.Serializable]
    public class MapRequestData
    {
        public int type = 2;
        public float lon;
        public float lat;

        public MapRequestData(float longitude, float latitude)
        {
            this.lon = longitude;
            this.lat = latitude;
        }
    }

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Websocket!!");
        initSocket();

        WebSocket.callbackFunc mapDataCallbackFunction = new WebSocket.callbackFunc((string error, string data) =>
        {
            Debug.Log(data);
            var chunkData = JsonUtility.FromJson<Assets.ChunkData>(data);

            //Debug.Log(chunkData.roads[0][0].lat);
            Debug.Log(chunkData.latitude_lower_bound);

            if (chunkData.roads == null)
                Debug.Log("Is unll!");
            else
                Debug.Log(chunkData.roads[0].nodes.Count);
        });

        WebSocket.callbackFunc loginCallbackFunction = new WebSocket.callbackFunc((string error, string data) =>
        {
            Debug.Log("FUCKING SUCCESS");
            Debug.Log(data);
            socket.send(JsonUtility.ToJson(new MapRequestData(17.01F, 51.1F)), mapDataCallbackFunction);
        });

        socket.send(JsonUtility.ToJson(new LoginData()), loginCallbackFunction);

        // TODO: FIX THIS DATA MANAGER
        /*
        int i = Assets.DataManager.instance().i;
        Assets.DataManager.instance().i = 200;
        Debug.Log(i);
        /*

        /*Assets.Messages.Message m = JsonUtility.FromJson
            <Assets.Messages.Message>("{\"message_type\": \"auth\", \"message\":\"ala ma kota\"}");

        Debug.Log("parsed: " + m.message_type + ", " + m.message);


        Assets.Messages.Message m2 = JsonUtility.FromJson
            <Assets.Messages.Message>("{\"message_type\": \"auth\", \"message\":\"ala ma kota\"}");

        Debug.Log("parsed: " + m.message_type + ", " + m.message);*/

        /*
        Assets.ChunkData m2 = JsonUtility.FromJson<Assets.ChunkData>
        ("{\"id\": 88,\"road_nodes\": [{\"lat_start\": 51.1685915, \"lat_end\": 51.1687291, \"lon_start\": 17.1102712, \"lon_end\": 17.1102226}, {\"lat_start\": 51.1687291, \"lat_end\": 51.1687663, \"lon_start\": 17.1102226, \"lon_end\": 17.1102016}]}");

        Debug.Log(JsonUtility.ToJson(m2));
        
        Assets.DataManager.instance().map.addChank(m2);
        Debug.Log("Global data: "+Assets.DataManager.instance().map.CountChunks);
        */
    }
	
	// Update is called once per frame
	void Update () {
        /*socket.processOrders();

        if (timer.isTimeEx(1000))
        {
            timer.update();
        }

        if (socket.isData)
        {
            //socket.getData() returns json from django
            Debug.Log("New data: "+ socket.getData());
        }*/
    }

    private void loadChunk()
    {

    }

    private void initSocket()
    {
        Debug.Log("Trying connect");
        socket = new Assets.Sockets.WebSocket();
        socket.connect(Const.SERVER_URL);
    }
}
