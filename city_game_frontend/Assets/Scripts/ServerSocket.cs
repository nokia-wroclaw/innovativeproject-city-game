using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class ServerSocket : MonoBehaviour {

    public MapManager mapManager; //= GameObject.Find("MapManager").GetComponent<MapManager>();
    public Assets.Sockets.WebSocket socket; //!< socket object

    private bool loggedIn; //!< true if socket is logged into django

    private Assets.TimerSync timer = new Assets.TimerSync();

    // retrieve chunk data class depending on given position
    WebSocket.callbackFunc mapDataCallbackFunction = new WebSocket.callbackFunc((string error, string data) =>
    {
        //Debug.Log(data);
        var chunkData = JsonUtility.FromJson<Assets.ChunkData>(data);

        //Debug.Log(chunkData.latitude_lower_bound);

        if (chunkData.roads == null)
            Debug.Log("Is unll!");
        else
            MapManager.chunksToDraw.Add(chunkData);

    });

    WebSocket.callbackFunc loginCallbackFunction = new WebSocket.callbackFunc((string error, string data) =>
    {
        Debug.Log("Login success!");
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
        socket = new Assets.Sockets.WebSocket();
        socket.connect(Const.SERVER_URL);
    }

    public void login()
    {
        socket.send(JsonUtility.ToJson(new LoginData()), loginCallbackFunction);
    }

    public void updateChunks(float longitude, float latitude)
    {
        socket.send(JsonUtility.ToJson(new MapRequestData(longitude, latitude)), mapDataCallbackFunction);
    }
}
