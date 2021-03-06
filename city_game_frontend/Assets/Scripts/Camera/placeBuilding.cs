﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEngine.EventSystems;

public class placeBuilding : MonoBehaviour {

    public GameObject groundPlane;
    public GameObject placableThing;
    public int currentTier;

    public static placeBuilding Instance;

    public Shader transparentShader;
    public Shader normalShader;

    public float rotationSpeed;

    private void Awake()
    {
        placeBuilding.Instance = this;
    }


    public void setStructureToBuild(GameObject structure, int tier)
    {
        if (placableThing != null)
            Destroy(placableThing);

        placableThing = Instantiate(structure);
        placableThing.transform.position = new Vector3(2000, 0, 2000);

        setShader(transparentShader);

        currentTier = tier;
    }

    private void setShader(Shader shaderToSet)
    {
        foreach(Renderer r in placableThing.GetComponentsInChildren<Renderer>())
        {
            r.material.shader = shaderToSet;
        }
        foreach (Renderer r in placableThing.GetComponents<Renderer>())
        {
            r.material.shader = shaderToSet;
        }
        
    }


    [ContextMenu("Confirm placement")]
    public void confirmBuildingPlacement()
    {
        setShader(normalShader);

        PlaceBuildingRequestData newBuildingData = new PlaceBuildingRequestData(
            Utils.GameCoordinateXToLatitude(placableThing.transform.position.x),
            Utils.GameCoordinateZToLongitude(placableThing.transform.position.z),
            placableThing.transform.rotation.eulerAngles.y,
            this.currentTier
        );

        ServerSocket.Instance.send(this.gameObject, JsonUtility.ToJson(newBuildingData), structurePlacementCallback);

        //TODO: SHOULD THIS BE HERE?
        PlayerActions.Instance.leaveBuildingMode();
        Destroy(placableThing);
    }

    Request.callbackFunc structurePlacementCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("STRUCTURE PLACEMENT CALLBACK");
        Debug.Log(error);
        Debug.Log(data);
    });
    
	// Update is called once per frame
	void Update () {

        //if (EventSystem.current.IsPointerOverGameObject())
            //return;

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            

            Touch touchZero = Input.GetTouch(0);


            Utils.rotationThatWorks(placableThing,
                new Vector3(
                    0,
                    touchZero.deltaPosition.x * rotationSpeed,
                    0
                    )
                );


        }

        // Move this object to the position clicked by the mouse.
        if (Input.touchCount == 2)
        {

            //Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay((Input.GetTouch(0).position + Input.GetTouch(1).position) / 2) ;  //(touch.position);// 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
        


                GameObject gameObjectClicked = hit.collider.gameObject;

                if (gameObjectClicked != groundPlane) {
                    Debug.Log("Wrong click");
                    return;

                }

                placableThing.transform.position = new Vector3(
                    hit.point.x,
                    placableThing.transform.position.y,
                    hit.point.z
                );



                Debug.Log(Utils.GameCoordinateXToLatitude(placableThing.transform.position.x));
                Debug.Log(Utils.GameCoordinateZToLongitude(placableThing.transform.position.z));

            }
        }
    }
}
