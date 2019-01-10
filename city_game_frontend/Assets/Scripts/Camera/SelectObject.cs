using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SelectObject : MonoBehaviour
{
    Collider coll;
    GameObject anchor;
    public GameObject playerCharacter;
    public static SelectObject Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coll = GetComponent<Collider>();
        anchor = GameObject.Find("/CameraAnchor/MainCamera");
    }

    void Update()
    {
        // Move this object to the position clicked by the mouse.
        if (Input.GetMouseButtonUp(0))
        {

            //Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //(touch.position);// 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //transform.position = ray.GetPoint(100.0f);
                //Debug.Log("Clicked");


                
                GameObject gameObjectClicked = hit.collider.gameObject;
                DynamicStructData dataOfObjectClicked = MapManager.GetDataOfObject(gameObjectClicked);


                if(dataOfObjectClicked != null || gameObjectClicked == playerCharacter)
                    anchor.GetComponent<cameraFollower>().changeObjectToFollow(
                        gameObjectClicked
                    );

                if(dataOfObjectClicked != null)
                {
                    // TODO: NOTIFY THE UI ABOUT THE STRUCTURE HERE
                    SimpleBuildingPanel.Instance.setOpen(dataOfObjectClicked);
                }

                if(gameObjectClicked == playerCharacter)
                {
                    // TODO: NOTIFY THE UI TO HIDE - THE PLAYER HAS BEEN FOCUSED
                    SimpleBuildingPanel.Instance.setClosed();
                }
            }
        }
    }
}
