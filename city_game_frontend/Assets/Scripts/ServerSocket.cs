using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class ServerSocket : MonoBehaviour {

    public static ServerSocket Instance { set; get; }

    GameManager gameManager;
    MapManager mapManager; //= GameObject.Find("MapManager").GetComponent<MapManager>();
    WebSocket socket; //!< socket object

    private bool loggedIn; //!< true if socket is logged into django

    private Assets.TimerSync timer = new Assets.TimerSync();

    Request.callbackFunc loginCallbackFunction = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Login success!");
        sender.GetComponent<ServerSocket>().gameManager.OnLogin();
        
    });

    private void Awake()
    {
        ServerSocket.Instance = this;

        Debug.Log("Socket server created!");
    }

    // Use this for initialization
    void Start ()
    {
        gameManager = GameManager.Instance;
        mapManager = MapManager.Instance;

        initSocket();
        login();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void send(GameObject sender, String data, Request.callbackFunc callback)
    {
        socket.send(sender, data, callback);
    }

    private void initSocket()
    {
        Debug.Log("Trying connect");
        socket = new WebSocket(Const.SERVER_URL);
    }

    public void login()
    {
        socket.send(gameObject, JsonUtility.ToJson(new LoginData()), loginCallbackFunction);
    }

}
