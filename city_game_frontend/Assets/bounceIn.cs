using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceIn : MonoBehaviour {

    float time = 0;
    public float speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * speed ;
        transform.Translate( new Vector3(0, -Mathf.Sin(time)*40 / (time*time), 0));
	}
}
