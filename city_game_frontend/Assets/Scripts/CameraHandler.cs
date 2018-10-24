using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public GameObject camera_GameObject;

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
                    Vector3 PositionDifference = NewPosition - StartPosition;
                    camera_GameObject.transform.Translate(-PositionDifference);
                }
                StartPosition = GetWorldPosition();
            }
        }
        else if (Input.touchCount == 2)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                isZooming = true;

                DragNewPosition = GetWorldPositionOfFinger(1);
                Vector3 PositionDifference = DragNewPosition - DragStartPosition;

                if (Vector3.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                        camera_GameObject.GetComponent<Camera>().orthographicSize += (PositionDifference.magnitude);

                if (Vector3.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                        camera_GameObject.GetComponent<Camera>().orthographicSize -= (PositionDifference.magnitude);

                DistanceBetweenFingers = Vector3.Distance(DragNewPosition, Finger0Position);
            }
            DragStartPosition = GetWorldPositionOfFinger(1);
            Finger0Position = GetWorldPositionOfFinger(0);
        }
    }

    Vector3 GetWorldPosition()
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    Vector3 GetWorldPositionOfFinger(int FingerIndex)
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    }
}
