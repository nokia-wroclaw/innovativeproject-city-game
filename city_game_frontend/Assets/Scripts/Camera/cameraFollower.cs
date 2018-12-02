using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollower : MonoBehaviour {

    GameObject anchor; // camera's parent object, used as the axis of rotation
    public GameObject player;
    GameObject objectToFollow;
    public float bias;
    float oneMinusBias;

    public float horizontalZoomSpeed;
    public float verticalZoomSpeed;
    public float rotationSpeed;

    // Use this for initialization
    void Start () {
        anchor = gameObject.transform.parent.gameObject;
        oneMinusBias = 1F - bias;
        objectToFollow = player;
	}
	
	// Update is called once per frame
	void Update () {
        anchor.transform.position = anchor.transform.position * bias + objectToFollow.transform.position * oneMinusBias;

        /*if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            float speed_x = touchZero.deltaPosition.x;
            float speed_y = touchZero.deltaPosition.y;

            anchor.transform.Rotate(
                new Vector3(
                    0,
                    touchZero.deltaPosition.x * rotationSpeed,
                    0//touchZero.deltaPosition.y
                    )
                );
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

            anchor.transform.localScale += new Vector3(
                anchor.transform.localScale.x * deltaMagnitudeDiff * verticalZoomSpeed * Time.deltaTime,
                anchor.transform.localScale.y * deltaMagnitudeDiff * horizontalZoomSpeed * Time.deltaTime,
                0
            );
        }*/
    }

    public void changeObjectToFollow(GameObject g)
    {
        objectToFollow = g;
    }

    void changeObjectToFollowToPlayer(GameObject g)
    {
        objectToFollow = player;
    }
}
