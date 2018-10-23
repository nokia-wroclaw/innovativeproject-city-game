using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSocket : MonoBehaviour {

    private Assets.Sockets.WebSocket socket; //!< socket object

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
        socket.processOrders();
    }

    private void loadChank()
    {

    }

    private void initSocket()
    {
        Debug.Log("Trying connect");
        socket = new Assets.Sockets.WebSocket();
        socket.connect(Const.SERVER_URL);

        socket.send("{\"login\":\"baczek\",\"pass\": \"baczekbezraczek\", \"type\":\"auth_event\"}");
    }
}
