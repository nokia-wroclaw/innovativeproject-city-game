using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelsContainerHandler : MonoBehaviour {

    public GameObject container;
    public GameObject playerPanel,guildPanel,skillTreePanel,actionsPanel,somethingPanel;

    public Text playerName, playerLvl, playerGuild, playerExp, playerRes1, playerRes2, playerRes3;

    public static PanelsContainerHandler Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void closeAllPanelsExcept(GameObject g)
    {
        playerPanel.SetActive(false);
        guildPanel.SetActive(false);
        skillTreePanel.SetActive(false);
        actionsPanel.SetActive(false);
        somethingPanel.SetActive(false);

        g.SetActive(true);
    }

    public void showContainer()
    {
        container.SetActive(true);
    }

    public void closeContainer()
    {
        container.SetActive(false);
    }

    public void guildPanelOn()
    {
        showContainer();
        closeAllPanelsExcept(guildPanel);
        //guildPanel.SetActive(true);
    }

    public void skillTreePanelOn()
    {
        showContainer();
        closeAllPanelsExcept(skillTreePanel);
    }

    public void actionsPanelOn()
    {
        showContainer();
        closeAllPanelsExcept(actionsPanel);
    }

    public void somethingPanelOn()
    {
        showContainer();
        closeAllPanelsExcept(somethingPanel);
    }

    public void playerPanelOn()
    {
        showContainer();
        closeAllPanelsExcept(playerPanel);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

	// Use this for initialization
	void Start () {
        container.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
