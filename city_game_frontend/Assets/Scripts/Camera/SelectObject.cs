using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SelectObject : MonoBehaviour
{
    Collider coll;
    GameObject anchor;

    void Start()
    {
        coll = GetComponent<Collider>();
        anchor = GameObject.Find("/CameraAnchor/MainCamera");
    }

    void Update()
    {
        // Move this object to the position clicked by the mouse.
        if (Input.GetMouseButtonDown(0))
        {

            //Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //(touch.position);// 
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0f))
            {
                //transform.position = ray.GetPoint(100.0f);
                Debug.Log("Clicked");

                anchor.GetComponent<cameraFollower>().changeObjectToFollow(this.gameObject);
            }
        }
    }
}
