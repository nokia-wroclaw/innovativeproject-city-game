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
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void loadChank()
    {

    }

    private void initSocket()
    {
        socket = new WebSocket("ws://"+Const.SERVER_IP + ":" + Const.SERVER_PORT);
        socket.Connect();

        
    }
}
