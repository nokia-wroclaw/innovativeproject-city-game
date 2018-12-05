using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBuildingPanel : MonoBehaviour {

    public static SimpleBuildingPanel Instance;

    public GameObject buildingPanel;
    public bool isOpen = false;
    public Sprite img = null;
    public string buildingName = null;

    private Text textObj;
    private Image imgObj;

    private DynamicStructData currentlyFocusedData = null;

    /*void OnMouseOver()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }*/

    private void Awake /* awake ooou this is the dream state */ ()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        buildingPanel.SetActive(false);
    }
	
    /*
	// Update is called once per frame
	void Update() {
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

            // Baczek: disabled this because it caused the panel to float above the viewport

            //Vector3 panelPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //panelPos.Set(panelPos.x,panelPos.y+=200, panelPos.z);
            //buildingPanel.transform.position = panelPos;
            //buildingPanel.transform.position.Set(buildingPanel.transform.position.x, buildingPanel.transform.position.y+130, buildingPanel.transform.position.z);
        }
        else
        {
            buildingPanel.SetActive(false);
        }
	}
    */

    public void setOpen(DynamicStructData data)
    {
        buildingPanel.SetActive(true);
        this.currentlyFocusedData = data;

        textObj = buildingPanel.GetComponentInChildren<Text>();
        textObj.text = getBuildingName(data);
    }

    public void setClosed()
    {
        buildingPanel.SetActive(false);
        this.currentlyFocusedData = null;
    }

    string getBuildingName(DynamicStructData structure)
    {
        if(structure.taken_over)
        {
            return structure.owner + "'s Mine, Level " + structure.tier.ToString();

        } else
        {
            Debug.Log("Ore, size " + structure.tier.ToString());
            return "Ore, size " + structure.tier.ToString();
        }
    }

    public void onTakeOverClicked()
    {
        Debug.Log("Takin ovah");
        PlayerActions.Instance.sendStructureTakeoverRequest(currentlyFocusedData.id);
    }
}
