using Assets.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public static PlayerActions Instance;

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

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
