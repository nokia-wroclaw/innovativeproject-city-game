using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsContainerHandler : MonoBehaviour {

    public GameObject container;

    public void showContainer()
    {
        container.SetActive(true);
    }

    public void closeContainer()
    {
        container.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        container.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
