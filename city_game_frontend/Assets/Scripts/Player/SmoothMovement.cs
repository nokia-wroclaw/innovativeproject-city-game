using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour {

    public Vector3 targetPosition;
    public Vector3 targetRotation;

    float bias = 0.90F;
    float oneMinusBias;

	// Use this for initialization
	void Start () {
        targetPosition = transform.position;
        targetRotation = transform.rotation.eulerAngles;
        oneMinusBias = 1 - bias;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Slerp(transform.position, targetPosition,oneMinusBias);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), oneMinusBias);
    }
}
