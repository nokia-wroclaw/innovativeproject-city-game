using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBuildingPanel : MonoBehaviour {

    public GameObject buildingPanel;
    public static bool isOpen = true;
    public static Sprite img = null;
    public static string buildingName = null;

    private Text textObj;
    private Image imgObj;

    /*void OnMouseOver()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }*/

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isOpen)
        {
            if (buildingName != null)
            {
                textObj = buildingPanel.GetComponentInChildren<Text>();
                textObj.text = buildingName;
            }
            if(img != null)
            {
                imgObj = buildingPanel.GetComponentInChildren<Image>();
                imgObj.sprite = img;
            }

            buildingPanel.SetActive(true);
            Vector3 panelPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //panelPos.Set(panelPos.x,panelPos.y+=200, panelPos.z);
            buildingPanel.transform.position = panelPos;
            //buildingPanel.transform.position.Set(buildingPanel.transform.position.x, buildingPanel.transform.position.y+130, buildingPanel.transform.position.z);
        }
        else
        {
            buildingPanel.SetActive(false);
        }
	}
}
