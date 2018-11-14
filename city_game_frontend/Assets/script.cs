using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {

    public float x = 0, y = 0, z = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("I am alive!");
        transform.Translate(new Vector3(x, y, z));
    }
}
