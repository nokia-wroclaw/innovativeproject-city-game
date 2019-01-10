using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeBuilding : MonoBehaviour {

    public GameObject groundPlane;
    public GameObject placableThing;

    public static placeBuilding Instance;

    public Shader transparentShader;
    public Shader normalShader;

    public float rotationSpeed;

    private void Awake()
    {
        placeBuilding.Instance = this;
    }

    private void OnEnable()
    {
        placableThing.GetComponent<Renderer>().material.shader = transparentShader;   
    }


    [ContextMenu("Confirm placement")]
    public void confirmBuildingPlacement()
    {
        placableThing.GetComponent<Renderer>().material.shader = normalShader;
        // TODO: SEND BUILDING DATA


        //TODO: SHOULD THIS BE HERE?
        PlayerActions.Instance.leaveBuildingMode();
    }

    // Use this for initialization
    void Start () {
        //transparentShader = Shader.Find("FX/Flare");
        //normalShader = Shader.Find("Standard");
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            

            Touch touchZero = Input.GetTouch(0);
            float speed_x = touchZero.deltaPosition.x;
            float speed_y = touchZero.deltaPosition.y;

            Debug.Log(speed_x);


            Utils.rotationThatWorks(placableThing,
                new Vector3(
                    0,
                    touchZero.deltaPosition.x * rotationSpeed,
                    0
                    )
                );


        }

        // Move this object to the position clicked by the mouse.
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            //Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);  //(touch.position);// 
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
                Debug.Log("Moving the prototype");


            }
        }
    }
}
