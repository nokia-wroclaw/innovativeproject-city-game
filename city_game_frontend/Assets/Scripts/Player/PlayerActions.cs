﻿using Assets.Sockets;
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
        EffectsManager.Instance.spawnEffect(
            MapManager.Instance.dynamicStructs[structureID].transform.position,
            5
            );

        ServerSocket.Instance.send(
            gameObject,
            JsonUtility.ToJson(
                new StructureTakeoverRequestData(structureID)
                ),
            structureTakeoverCallback);
    }

    public void enterBuildingMode()
    {
        cameraFollower.Instance.enabled = false;
        SelectObject.Instance.enabled = false;
        placeBuilding.Instance.enabled = true;
    }

    public void leaveBuildingMode()
    {
        placeBuilding.Instance.enabled = false;
        cameraFollower.Instance.enabled = true;
        SelectObject.Instance.enabled = true;

        Destroy(placeBuilding.Instance.placableThing);
    }


    

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
