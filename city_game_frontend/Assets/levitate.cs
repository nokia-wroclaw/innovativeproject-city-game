using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levitate : MonoBehaviour {

    public float speed;
    public float amplitude;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(0, Mathf.Sin(Time.timeSinceLevelLoad * speed) * amplitude, 0);
	}
}
