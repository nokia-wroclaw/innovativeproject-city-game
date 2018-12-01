using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour {

    float previousDistance;
    public float zoomSpeed = 1f;

    public GameObject player;

    float pinchAmount = 0;
    float previousPinch = 0;

    enum Direction
    {
        growing,
        decreasing,
        unknown
    }

    Direction direction = Direction.unknown;

	// Update is called once per frame
	void FixedUpdate () {
		if(Input.touchCount == 2 &&
            (Input.GetTouch(0).phase == TouchPhase.Began ||
            Input.GetTouch(1).phase == TouchPhase.Began))
        {
            //calibrate distance
            previousDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }

        else if (Input.touchCount == 2 &&
            (Input.GetTouch(0).phase == TouchPhase.Moved ||
            Input.GetTouch(1).phase == TouchPhase.Moved))
        {
            float distance;
            Vector2 touch1 = Input.GetTouch(0).position;
            Vector2 touch2 = Input.GetTouch(1).position;

            distance = Vector2.Distance(touch1, touch2);

            Debug.Log(calculateDistance());


                if (previousDistance != 0 && pinchAmount != 0)
                     direction = actualDirection();




            float newPinch = -(previousDistance - distance) * zoomSpeed * (calculateDistance() / 1000);// (1 / calculateDistance());

            

                previousPinch = pinchAmount;
                pinchAmount = newPinch;// * Time.deltaTime;

           // Debug.Log(previousPinch + "->" + pinchAmount + " " +direction+ " "+isFlow(pinchAmount, newPinch));

            
                if (!Camera.main.orthographic)
                {
                    if (calculateDistance() -pinchAmount < 100f &&
                        calculateDistance() - pinchAmount > 8f)
                    {
                        Camera.main.transform.Translate(0, 0, pinchAmount);
                    }
                }
                else
                {
                    Debug.Log("Camera is orthographic! Change it.");

                }



            
        }

            

            

        

    }

    float calculateDistance()
    {
        Vector3 vecCam = Camera.main.transform.position;
        Vector3 vecPlayer = player.transform.position;

        return Vector3.Distance(vecCam, vecPlayer);
    }

    bool isFlow(float previous, float actual)
    {
        if ((previous < actual) && direction == Direction.growing)
            return true;
        else if ((previous > actual) && direction == Direction.decreasing)
            return true;

        return false;
    }

    Direction actualDirection()
    {
        return (previousPinch < pinchAmount) ? Direction.growing : Direction.decreasing;
    }
}
