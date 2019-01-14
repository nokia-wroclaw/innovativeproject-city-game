using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollower : MonoBehaviour {

    GameObject anchor; // camera's parent object, used as the axis of rotation
    public GameObject player;
    GameObject objectToFollow;
    public float bias;
    float oneMinusBias;

    public float zoomSpeed;
    public float horizontalRotationSpeed;
    public float verticalRotationSpeed;

    const float MIN_X_ROTATION = 1;
    const float MAX_X_ROTATION = 60;

    public static cameraFollower Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        anchor = gameObject.transform.parent.gameObject;
        oneMinusBias = 1F - bias;
        objectToFollow = player;
	}
	
	// Update is called once per frame
	void Update () {

        if(objectToFollow == null)
        {
            objectToFollow = player;
        }

        anchor.transform.position = anchor.transform.position * bias + objectToFollow.transform.position * oneMinusBias;

        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            float speed_x = touchZero.deltaPosition.x;
            float speed_y = touchZero.deltaPosition.y;
            //float speed_z = touchZero.deltaPosition.z;

            //Debug.Log(speed_x + " " + speed_y);// + " " + speed_z);

            /*
         rotationThatWorks( anchor,
            new Vector3(
                0,
                touchZero.deltaPosition.x * horizontalRotationSpeed,
                0
                )
        );
        */

        Utils.rotationThatWorks(anchor,
            new Vector3(
                -touchZero.deltaPosition.y * verticalRotationSpeed,
                touchZero.deltaPosition.x * horizontalRotationSpeed,
                0
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

            var newDistance = anchor.transform.localScale + new Vector3(
                0,//anchor.transform.localScale.x * deltaMagnitudeDiff * verticalZoomSpeed * Time.deltaTime,
                0,//anchor.transform.localScale.y * deltaMagnitudeDiff * horizontalZoomSpeed * Time.deltaTime,
                 anchor.transform.localScale.z * deltaMagnitudeDiff * Time.deltaTime * zoomSpeed
            );

            if (isInPinchRange(newDistance))
                anchor.transform.localScale = newDistance;

            //Debug.Log(anchor.transform.localScale);
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            Utils.rotationThatWorks(anchor, new Vector3(
                            verticalRotationSpeed,
                            0,
                            0
                            )
                        );
            /*
            anchor.transform.Rotate(
                             new Vector3(
                                 verticalRotationSpeed,
                                 0,
                                 0
                                 )
                                 //,Space.World
                             );
                             */
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Utils.rotationThatWorks(anchor, new Vector3(
                                 -verticalRotationSpeed,
                                 0,
                                 0
                                 )
                             );
            /*
            anchor.transform.Rotate(
                             new Vector3(
                                 -verticalRotationSpeed,
                                 0,
                                 0
                                 )
                                 //,Space.World
                             );
                             */
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Utils.rotationThatWorks(anchor, new Vector3(
                                 0,
                                 -horizontalRotationSpeed * 3,
                                 0
                                 ));
            /*
            anchor.transform.Rotate(
                             new Vector3(
                                 0,
                                 -horizontalRotationSpeed * 3,
                                 0
                                 )
                                 //,Space.World
                             );
                             */
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Utils.rotationThatWorks(anchor, new Vector3(
                                0,
                                +horizontalRotationSpeed * 3,
                                0
                                ));
            /*
            anchor.transform.Rotate(
                             new Vector3(
                                 0,
                                 +horizontalRotationSpeed * 3,
                                 0
                                 )
                                 //,Space.World
                             );

    */
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            var newDistance = anchor.transform.localScale + new Vector3(
               0,0,
               -anchor.transform.localScale.z * Time.deltaTime * zoomSpeed
           );

            if (isInPinchRange(newDistance))
                anchor.transform.localScale = newDistance;

        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            var newDistance = anchor.transform.localScale + new Vector3(
              0,0,
              anchor.transform.localScale.z * Time.deltaTime * zoomSpeed
           );

            if (isInPinchRange(newDistance))
                anchor.transform.localScale = newDistance;

        }


        
        // Rotation boundaries
        if (anchor.transform.eulerAngles.x < MIN_X_ROTATION)
        {
            anchor.transform.eulerAngles = new Vector3(
               MIN_X_ROTATION,
               anchor.transform.eulerAngles.y,
               0
            );
        }


        if (anchor.transform.eulerAngles.x > MAX_X_ROTATION)
        {
            anchor.transform.eulerAngles = new Vector3(
                MAX_X_ROTATION,
                anchor.transform.eulerAngles.y,
                0
            );
        }
        


    }

    public bool isInPinchRange(Vector3 dist)
    {
        return (dist.x >= 0.2 && dist.x <= 2.0);
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
