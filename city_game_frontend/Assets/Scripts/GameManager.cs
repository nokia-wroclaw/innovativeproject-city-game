using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;



public class GameManager : MonoBehaviour {

    public ServerSocket server;
    public MapManager mapManager;
    //public GPSManager gpsManager; // TODO

    /*
     * TEMPORARILY STORING THE GPS CORDS HERE FOR EASIER DEBUGGING
     */
    public float gps_lat = 51.106840F;
    public float gps_lon = 17.093805F;

    public float current_chunk_lat = -10000;
    public float current_chunk_lon = -10000;

    // TEMPORARY LOCATION INDICATOR
    public GameObject locationIndicator;

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

    public void OnLocationChanged(float lon, float lat)
    {

        locationIndicator.transform.position = new Vector3(
            MapManager.LatitudeToGameCoordinate(gps_lat),
            2,
            MapManager.LongitudeToGameCoordinate(gps_lon)

            );

        if (roundDownToChunkCords(lon) == current_chunk_lon && roundDownToChunkCords(lat) == current_chunk_lat)
        {
            Debug.Log("Location has changed, but you are still on the same chunk!");
            Debug.Log("Won't draw now..");

        }
        else 
        {
            Debug.Log("Location changed");

            current_chunk_lat = roundDownToChunkCords(gps_lat);
            current_chunk_lon = roundDownToChunkCords(gps_lon);

            server.sendChunkRequest(gps_lon, gps_lat);

        }


    }


    [ContextMenu("On Location Changed")]
    public void forceOnLocationChanged()
    {
        this.OnLocationChanged(gps_lon, gps_lat);
    }

    float roundDownToChunkCords(float x)
    {
        return Mathf.Floor(x * 100) / 100;
    }
}
