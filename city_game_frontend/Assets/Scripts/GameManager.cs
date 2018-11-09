using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }

    ServerSocket server;
    MapManager mapManager;

    public bool fakeLocation = false;
    public float fake_lat = 51.107621F;
    public float fake_lon = 17.103190F;

    public float current_chunk_lat = -10000;
    public float current_chunk_lon = -10000;

    // TEMPORARY LOCATION INDICATOR
    public GameObject locationIndicator;

    // TODO: Find a way to make this non-static
    public static Queue<Request> callbacksToProcess = new Queue<Request>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        server = ServerSocket.Instance;
        mapManager = MapManager.Instance;
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

        if (fakeLocation)
        {
            lon = fake_lon;
            lat = fake_lat;
        }


        locationIndicator.transform.position = new Vector3(
            MapManager.LatitudeToGameCoordinate(lat),
            2,
            MapManager.LongitudeToGameCoordinate(lon)

            );

        
        if (roundDownToChunkCords(lon) == current_chunk_lon && roundDownToChunkCords(lat) == current_chunk_lat)
        {
            Debug.Log("Location has changed, but you are still on the same chunk!");
            Debug.Log("Won't draw now..");

        }
        else 
        {
            Debug.Log("Location changed");

            current_chunk_lat = roundDownToChunkCords(lat);
            current_chunk_lon = roundDownToChunkCords(lon);

            Debug.Log(server == null);
            server.sendChunkRequest(lon, lat);


            server.sendChunkRequest(lon + Const.CHUNK_SIZE, lat + Const.CHUNK_SIZE);
            server.sendChunkRequest(lon + Const.CHUNK_SIZE, lat - Const.CHUNK_SIZE);
            server.sendChunkRequest(lon - Const.CHUNK_SIZE, lat + Const.CHUNK_SIZE);
            server.sendChunkRequest(lon - Const.CHUNK_SIZE, lat - Const.CHUNK_SIZE);

            server.sendChunkRequest(lon, lat + Const.CHUNK_SIZE);
            server.sendChunkRequest(lon, lat - Const.CHUNK_SIZE);
            server.sendChunkRequest(lon + Const.CHUNK_SIZE, lat);
            server.sendChunkRequest(lon - Const.CHUNK_SIZE, lat);


        }


    }


    [ContextMenu("On Location Changed")]
    public void forceOnLocationChanged()
    {
        Debug.Log("Currently disabled");
        /*
        this.OnLocationChanged(lat, lon);
        */
    }

    float roundDownToChunkCords(float x)
    {
        return Mathf.Floor(x * 100) / 100;
    }
}
