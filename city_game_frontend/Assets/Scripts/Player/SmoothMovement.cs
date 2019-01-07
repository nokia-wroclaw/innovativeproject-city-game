using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour {

    Vector3 targetPosition;
    Quaternion targetRotation;

    float lerpSpeed = 2F;

    public void setTargetPosition(float x, float z)
    {
        this.targetPosition = new Vector3(
            x,
            transform.position.y,
            z
        );

    }

    public void setTargetRotation(float rotation)
    {
        rotation = -rotation - 90;

        Vector3 currentRotation = transform.rotation.eulerAngles;
        this.targetRotation = Quaternion.Euler(currentRotation.x, rotation, currentRotation.z);
    }


    // Use this for initialization
    void Start () {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
    }
}
