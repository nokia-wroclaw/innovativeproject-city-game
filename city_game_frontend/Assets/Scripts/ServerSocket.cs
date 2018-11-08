using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class ServerSocket : MonoBehaviour {

    public GameManager gameManager;
    public MapManager mapManager; //= GameObject.Find("MapManager").GetComponent<MapManager>();
    public WebSocket socket; //!< socket object

    private bool loggedIn; //!< true if socket is logged into django

    private Assets.TimerSync timer = new Assets.TimerSync();

    // retrieve chunk data class depending on given position
    Request.callbackFunc mapDataCallbackFunction = new Request.callbackFunc((GameObject sender,string error, string data) =>
    {
        
        //Debug.Log(data);
        var chunkData = JsonUtility.FromJson<Assets.ChunkData>(data);

        //Debug.Log(chunkData.latitude_lower_bound);

        sender.GetComponent<ServerSocket>().mapManager.drawChunk(chunkData);

    });

    Request.callbackFunc loginCallbackFunction = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Login success!");
        sender.GetComponent<ServerSocket>().gameManager.OnLogin();
        
    });
    
    // Use this for initialization
    void Start ()
    {
        
        initSocket();

        login();
        

 
    }
	
	// Update is called once per frame
	void Update () {

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

    public void sendChunkRequest(float longitude, float latitude)
    {
        socket.send(gameObject, JsonUtility.ToJson(new MapRequestData(longitude, latitude)), mapDataCallbackFunction);
    }
}
