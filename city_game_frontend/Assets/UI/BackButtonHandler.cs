using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour {

    public Animator mainPanel;

    public void ButtonPressed()
    {
        if(mainPanel.GetBool("isPanelOpen") == true)
        {
            mainPanel.SetBool("isPanelOpen", false);
        }
        else if(mainPanel.GetBool("isPanelOpen") == false)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
