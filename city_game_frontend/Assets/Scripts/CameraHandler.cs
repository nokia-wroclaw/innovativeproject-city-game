using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public GameObject camera;
    public GameObject player;




    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;







    Vector3 StartPosition;
    Vector3 DragStartPosition;
    Vector3 DragNewPosition;
    Vector3 Finger0Position;
    float DistanceBetweenFingers;
    bool isZooming;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 && isZooming)
        {
            isZooming = false;
        }

        if (Input.touchCount == 1)
        {
            if (!isZooming)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector3 NewPosition = GetWorldPosition();
                    float PositionDifference = NewPosition.x - StartPosition.x;
                    camera.transform.Rotate(new Vector3(0, PositionDifference, 0));
                }
                StartPosition = GetWorldPosition();
                Debug.Log("Hello");
            }
        }



        else if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            
                // Otherwise change the field of view based on the change in distance between the touches.
                //camera.GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
            camera.GetComponent<Camera>().transform.Translate(new Vector3(0, deltaMagnitudeDiff * perspectiveZoomSpeed, -1 * deltaMagnitudeDiff * perspectiveZoomSpeed), player.transform);


            // Clamp the field of view to make sure it's between 0 and 180.
            camera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(camera.GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            
        }



    }

    Vector3 GetWorldPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 20; // select distance = 10 units from the camera
        return camera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
    }

    Vector3 GetWorldPositionOfFinger(int FingerIndex)
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 20; // select distance = 10 units from the camera
        return camera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);//(Input.GetTouch(FingerIndex).position);
    }
}


/*
             if (Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                isZooming = true;

                DragNewPosition = GetWorldPositionOfFinger(1);
                Vector3 PositionDifference = DragNewPosition - DragStartPosition;
                
                if (Vector3.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                {
                    //camera_GameObject.GetComponent<Camera>().fieldOfView += 5;// ((PositionDifference.magnitude)*30);
                    //float a = PositionDifference.magnitude;
                    transform.Translate(new Vector3(0, PositionDifference.magnitude * 30, -1 * PositionDifference.magnitude * 30), player.transform);
                    Debug.Log("1");
                }
                /*
                if (Vector3.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                {
                    //camera_GameObject.GetComponent<Camera>().fieldOfView -= 5;//((PositionDifference.magnitude) * 30);
                    transform.Translate(new Vector3(0, -1*PositionDifference.magnitude * 30, PositionDifference.magnitude * 30), player.transform);
                    Debug.Log("2");
               

DistanceBetweenFingers = Vector3.Distance(DragNewPosition, Finger0Position);
            }
            DragStartPosition = GetWorldPositionOfFinger(1);
Finger0Position = GetWorldPositionOfFinger(0);
 * */