using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour {

    public Vector3 targetPosition;
    float bias = 0.90F;
    float oneMinusBias;

	// Use this for initialization
	void Start () {
        targetPosition = transform.position;
        oneMinusBias = 1 - bias;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position * bias + targetPosition * oneMinusBias;
    }
}
