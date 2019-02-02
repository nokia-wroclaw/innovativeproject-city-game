using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsPanelHandler : MonoBehaviour {

    public GameObject b1, b2, b3;

    public void build1()
    {
        PanelsContainerHandler.Instance.closeContainer();
        PlayerActions.Instance.enterBuildingMode();
        placeBuilding.Instance.setStructureToBuild(b1, 1);
    }

    public void build2()
    {
        PanelsContainerHandler.Instance.closeContainer();
        PlayerActions.Instance.enterBuildingMode();
        placeBuilding.Instance.setStructureToBuild(b2, 2);
    }

    public void build3()
    {
        PanelsContainerHandler.Instance.closeContainer();
        PlayerActions.Instance.enterBuildingMode();
        placeBuilding.Instance.setStructureToBuild(b3, 3);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
