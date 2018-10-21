using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSocket : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        int i = Assets.DataManager.instance().getI();
        Assets.DataManager.instance().setI(20);
        Debug.Log(i);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
