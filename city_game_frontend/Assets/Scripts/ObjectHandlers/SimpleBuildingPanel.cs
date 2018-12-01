using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBuildingPanel : MonoBehaviour {

    public GameObject buildingPanel;
    bool isMouseOver = false;

    void OnMouseOver()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isMouseOver)
        {
            buildingPanel.SetActive(true);
            Vector3 panelPos = Camera.main.WorldToScreenPoint(this.transform.position);
            buildingPanel.transform.position = panelPos;
        }
        else
        {
            buildingPanel.SetActive(false);
        }
	}
}
