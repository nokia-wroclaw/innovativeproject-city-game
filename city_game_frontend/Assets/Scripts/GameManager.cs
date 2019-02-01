using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }

    ServerSocket server;
    MapManager mapManager;

    public float current_chunk_lat = -10000;
    public float current_chunk_lon = -10000;

    public bool isLoggedIn = false;

    // TEMPORARY LOCATION INDICATOR
    public GameObject locationIndicator;
    SmoothMovement locationIndicatorMovementScript;

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
        locationIndicatorMovementScript = locationIndicator.GetComponent<SmoothMovement>();
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

            if (eventToHandle.getResponseData().isSpecialMessage)
            {
                handleSpecialEvent(eventToHandle);
                return;
            }

            eventToHandle.performCallback();
        }
    }

    void handleSpecialEvent(Request eventToHandle)
    {
        Debug.Log("Handling special event!");
        var responseData = eventToHandle.getResponseData();

        if(responseData.specialMessageID == Const.SPECIAL_MESSAGE_MAP_UPDATE)
        {
            Debug.Log("Received a server-driven map update!");
            MapManager.Instance.structsDataCallbackFunction(gameObject, "", responseData.message);
        }

        if(responseData.specialMessageID == Const.SPECIAL_MESSAGE_GUILD_MEMBER_POSITION_UPDATE)
        {
            MapManager.Instance.handleGuildMemberLocationUpdate(responseData.message);
        }


        if (responseData.specialMessageID == Const.SPECIAL_MESSAGE_GUILD_INVITE_NOTIFICATION)
        {
            Debug.Log("invite received!");
            GuildInvite inviteData = JsonUtility.FromJson<GuildInvite>(responseData.message);
            Debug.Log(inviteData.guild_name);
            //TUTAJ KRZYŚ MOŻE HANDLOWAĆ UI
        }
    }

    
    public void OnLogin()
    {
        isLoggedIn = true;
    }

    public void OnLocationChanged(float lon, float lat, float rotation)
    {

        locationIndicatorMovementScript.setTargetPosition(Utils.LatitudeToGameCoordinate(lat), Utils.LongitudeToGameCoordinate(lon));
        

        Vector3 currentIndicatorRotation = locationIndicator.transform.rotation.eulerAngles;

        locationIndicatorMovementScript.setTargetRotation(rotation);

        server.send(gameObject, JsonUtility.ToJson(new LocationUpdateRequestData(lon, lat, rotation)), locationReportCallback);
        
        if (roundDownToChunkCords(lon) == current_chunk_lon && roundDownToChunkCords(lat) == current_chunk_lat)
        {
            //Debug.Log("Location has changed, but you are still on the same chunk!");
            //Debug.Log("Won't draw now..");

        }
        else 
        {
            //Debug.Log("Location changed");

            current_chunk_lat = roundDownToChunkCords(lat);
            current_chunk_lon = roundDownToChunkCords(lon);


            mapManager.sendChunkRequest(lon, lat);


            mapManager.sendChunkRequest(lon + Const.CHUNK_SIZE, lat + Const.CHUNK_SIZE);
            mapManager.sendChunkRequest(lon + Const.CHUNK_SIZE, lat - Const.CHUNK_SIZE);
            mapManager.sendChunkRequest(lon - Const.CHUNK_SIZE, lat + Const.CHUNK_SIZE);
            mapManager.sendChunkRequest(lon - Const.CHUNK_SIZE, lat - Const.CHUNK_SIZE);

            mapManager.sendChunkRequest(lon, lat + Const.CHUNK_SIZE);
            mapManager.sendChunkRequest(lon, lat - Const.CHUNK_SIZE);
            mapManager.sendChunkRequest(lon + Const.CHUNK_SIZE, lat);
            mapManager.sendChunkRequest(lon - Const.CHUNK_SIZE, lat);


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

    Request.callbackFunc locationReportCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        //TODO: HANDLE
    });
    
}
