using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;

public class ServerSocket : MonoBehaviour {

    private WebSocket socket; //!< socket object

    private bool loggedIn; //!< true if socket is logged into djungo

	// Use this for initialization
	void Start ()
    {
        int i = Assets.DataManager.instance().getI();
        Assets.DataManager.instance().setI(20);
        Debug.Log(i);

        initSocket();

        /*using (var ws = new WebSocket("ws://127.0.0.1:8000/ws/"))
        {
            Debug.Log("loading");
            ws.OnMessage += (sender, e) =>
                Debug.Log("Laputa says: " + e.Data);

            ws.Connect();
            ws.OnMessage += (sender, e) => 
                ws.Send("BALUS");
            //Debug.ReadKey(true);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void loadChank()
    {

    }

    private void initSocket()
    {
        socket = new WebSocket("ws://"+Const.SERVER_URL);
        
        //socket.Send("{'login':'baczek','pass': 'baczekbezraczek'}");
        socket.OnOpen += (m, e) =>
        {
            socket.Send("{\"login\":\"baczek\",\"pass\": \"baczekbezraczek\", \"type\":\"auth_event\"}");

            Debug.Log("Connected!!");
        };

        socket.OnMessage += (m, e) =>
        {

            Debug.Log(e.Data);

        };

        socket.Connect();
    }
}
