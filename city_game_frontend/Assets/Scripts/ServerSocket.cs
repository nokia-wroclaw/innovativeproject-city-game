using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSocket : MonoBehaviour {

    private Assets.Sockets.WebSocket socket; //!< socket object

    private bool loggedIn; //!< true if socket is logged into djungo

    private Assets.TimerSync timer = new Assets.TimerSync();

	// Use this for initialization
	void Start ()
    {

        int i = Assets.DataManager.instance().getI();
        Assets.DataManager.instance().setI(20);
        Debug.Log(i);

        initSocket();

        /*Assets.Messages.Message m = JsonUtility.FromJson
            <Assets.Messages.Message>("{\"message_type\": \"auth\", \"message\":\"ala ma kota\"}");

        Debug.Log("parsed: " + m.message_type + ", " + m.message);


        Assets.Messages.Message m2 = JsonUtility.FromJson
            <Assets.Messages.Message>("{\"message_type\": \"auth\", \"message\":\"ala ma kota\"}");

        Debug.Log("parsed: " + m.message_type + ", " + m.message);*/

        Assets.ChunkData m2 = JsonUtility.FromJson<Assets.ChunkData>
        ("{\"id\": 88,\"road_nodes\": [{\"lat_start\": 51.1685915, \"lat_end\": 51.1687291, \"lon_start\": 17.1102712, \"lon_end\": 17.1102226}, {\"lat_start\": 51.1687291, \"lat_end\": 51.1687663, \"lon_start\": 17.1102226, \"lon_end\": 17.1102016}]}");

        Debug.Log(JsonUtility.ToJson(m2));

        Debug.Log(m2.road_nodes.Count);
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
            //socket.getData() returns json from djungo
            Debug.Log("New data: "+ socket.getData());
        }*/
    }

    private void loadChank()
    {

    }

    private void initSocket()
    {
        Debug.Log("Trying connect");
        socket = new Assets.Sockets.WebSocket();
        socket.connect(Const.SERVER_URL);

        socket.sendLogReq("baczek", "baczekbezraczek");
        socket.sendChunkReq(17.11, 51.18);
    }
}
