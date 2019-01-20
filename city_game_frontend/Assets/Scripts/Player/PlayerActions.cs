using Assets.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public static PlayerActions Instance;
    public PlayerData currentPlayerData;

    private void Awake()
    {
        PlayerActions.Instance = this;
    }

    public Request.callbackFunc structureTakeoverCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Structure takeover: " + data);
    });

    public void sendStructureTakeoverRequest(int structureID)
    {
        ServerSocket.Instance.send(
            gameObject,
            JsonUtility.ToJson(
                new StructureTakeoverRequestData(structureID)
                ),
            structureTakeoverCallback);
    }


    public void sendCreateGuildRequest(string guildName)
    {
        ServerSocket.Instance.send(
            gameObject,
            JsonUtility.ToJson(
                new GuildCreationRequestData(guildName)
                ), guildCreationCallback);
    }

    public Request.callbackFunc guildCreationCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Guild created: " + data);
    });

    public void enterBuildingMode()
    {
        cameraFollower.Instance.enabled = false;
        SelectObject.Instance.enabled = false;
        placeBuilding.Instance.enabled = true;
    }

    public void leaveBuildingMode() {
        cameraFollower.Instance.enabled = true;
        SelectObject.Instance.enabled = true;
        placeBuilding.Instance.enabled = false;
    }


    public void getPlayerData()
    {
        ServerSocket.Instance.send(this.gameObject, JsonUtility.ToJson(new PlayerDataRequestData()), playerDataRequestCallback);
    }

    public Request.callbackFunc playerDataRequestCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        PlayerData retrievedPlayerData = JsonUtility.FromJson<PlayerData>(data);

        sender.GetComponent<PlayerActions>().currentPlayerData = retrievedPlayerData;
    });

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
