using Assets.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour {

    public static PlayerDataManager Instance;

    public PlayerData currentPlayerData;

    public void Awake()
    {
        Instance = this;
    }

    public void refreshPlayerData()
    {

        if (GameManager.Instance.isLoggedIn)
            ServerSocket.Instance.send(this.gameObject, JsonUtility.ToJson(new PlayerDataRequestData()), playerDataRequestCallback);
    }

    private Request.callbackFunc playerDataRequestCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("PLAYER DATA:");
        Debug.Log(data);
        PlayerData retrievedPlayerData = JsonUtility.FromJson<PlayerData>(data);


        sender.GetComponent<PlayerDataManager>().currentPlayerData = retrievedPlayerData;
        PanelsContainerHandler.Instance.playerName.text = retrievedPlayerData.name;
        PanelsContainerHandler.Instance.playerExp.text = retrievedPlayerData.exp.ToString();
        PanelsContainerHandler.Instance.playerLvl.text = retrievedPlayerData.level.ToString();
        PanelsContainerHandler.Instance.playerGuild.text = retrievedPlayerData.guild;
        PanelsContainerHandler.Instance.playerRes1.text = retrievedPlayerData.Cementia.ToString();
        PanelsContainerHandler.Instance.playerRes2.text = retrievedPlayerData.Plasmatia.ToString();
        PanelsContainerHandler.Instance.playerRes3.text = retrievedPlayerData.Auferia.ToString();
        PanelsContainerHandler.Instance.refreshExp(PlayerDataManager.Instance);
        PanelsContainerHandler.Instance.refreshHP();

        //PlayersBarHandler.Instance.refreshExp(retrievedPlayerData);
    });

    // Use this for initialization
    void Start () {
        InvokeRepeating("refreshPlayerData", 1, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
