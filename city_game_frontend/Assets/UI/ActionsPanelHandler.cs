using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsPanelHandler : MonoBehaviour {

    public GameObject b1, b2, b3;

    public GameObject twoButtonsPanel;
    public GameObject playerButton, plank, OtherButtons;

    public void whenBuildingModeOn()
    {
        PanelsContainerHandler.Instance.closeContainer();
        twoButtonsPanel.SetActive(true);
        playerButton.SetActive(false);
        plank.SetActive(false);
        OtherButtons.SetActive(false);
    }

    public void whenBuildingModeOff()
    {
        PanelsContainerHandler.Instance.closeContainer();
        twoButtonsPanel.SetActive(false);
        playerButton.SetActive(true);
        plank.SetActive(true);
        OtherButtons.SetActive(true);
    }

    public void placeTheBuilding()
    {
        placeBuilding.Instance.confirmBuildingPlacement();
        whenBuildingModeOff();
    }

    public void exitBuildingMode()
    {
        PlayerActions.Instance.leaveBuildingMode();
        whenBuildingModeOff();
    }

    public void build1()
    {
        whenBuildingModeOn();
        PlayerActions.Instance.enterBuildingMode();
        placeBuilding.Instance.setStructureToBuild(b1, 1);
    }

    public void build2()
    {
        whenBuildingModeOn();
        PlayerActions.Instance.enterBuildingMode();
        placeBuilding.Instance.setStructureToBuild(b2, 2);
    }

    public void build3()
    {
        whenBuildingModeOn();
        PlayerActions.Instance.enterBuildingMode();
        placeBuilding.Instance.setStructureToBuild(b3, 3);
    }

    // Use this for initialization
    void Start () {
        twoButtonsPanel.SetActive(false);
	}
}
