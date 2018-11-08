using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;



public class GameManager : MonoBehaviour {

    public ServerSocket server;
    public MapManager mapManager;
    //public GPSManager gpsManager; // TODO

    // TODO: Find a way to make this non-static
    public static Queue<Request> callbacksToProcess = new Queue<Request>();

    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
        ProcessCallbacks();
    }

    void ProcessCallbacks()
    {
        if (callbacksToProcess.Count > 0)
        {
            Request eventToHandle = callbacksToProcess.Dequeue();
            eventToHandle.performCallback();
        }
    }

    public void OnLogin()
    {
        
    }
}
