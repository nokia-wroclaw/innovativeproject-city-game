using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelButtonHandler : MonoBehaviour {

    public Animator mainPanel;

    private bool isOpen = false;

    public bool GetIsOpen()
    {
        return isOpen;
    }

    public void OpenMainPanel()
    {
        mainPanel.SetBool("isPanelOpen", true);
        isOpen = true;
    }

    public void CloseMainPanel()
    {
        mainPanel.SetBool("isPanelOpen", false);
        isOpen = false;
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
