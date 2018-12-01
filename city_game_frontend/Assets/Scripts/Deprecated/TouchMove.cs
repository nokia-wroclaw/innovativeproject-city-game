using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour {

    // Update is called once per frame
        public GameObject target;//the target object
        private float speedMod = 5.0f;//a speed modifier
        private Vector3 point;//the coord to the point where the camera looks at

        Vector3 StartPosition;

        void Start()
        {//Set up things on the start method
            point = target.transform.position;//get target's coords
            transform.LookAt(point);//makes the camera look to it
        }

        void Update()
        {//makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier

        if (Input.touchCount == 1)
        {
            
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector3 NewPosition = GetWorldPosition();
                    Vector3 PositionDifference = NewPosition - StartPosition;
                //if(PositionDifference.x > 0.01f)
                //{
                    Vector3 fixedTransition = new Vector3(0, PositionDifference.x, 0);
                    transform.RotateAround(point, fixedTransition, 0.5f * speedMod);
                //}
                
                }
                StartPosition = GetWorldPosition();
            
        }

        
    }

    Vector3 GetWorldPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = calculateDistance(); // select distance = 10 units from the camera
        return transform.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
    }

    float calculateDistance()
    {
        Vector3 vecCam = Camera.main.transform.position;
        Vector3 vecPlayer = target.transform.position;

        return Vector3.Distance(vecCam, vecPlayer);
    }

}
