using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forward : MonoBehaviour {

    public float speedX;
    public float speedY;
    public float speedZ;



    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (gameObject.name == "knightOLD" && speedX < 0)
        {
            speedX *= -1;
        }

        transform.Translate(new Vector3(speedX, 0, 0)  * Time.deltaTime);
        transform.Translate(new Vector3(0, speedY, 0)  * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, speedZ)  * Time.deltaTime);

    }
}
