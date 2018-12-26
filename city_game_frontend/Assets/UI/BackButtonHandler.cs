using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{

    public Animator mainPanel, guildPanel;

    public void ButtonPressed()
    {
        if (mainPanel.GetBool("isPanelOpen") == true)
        {
            mainPanel.SetBool("isPanelOpen", false);
        }
        else if (guildPanel.GetBool("isGuildPanelOpen") == true)
        {
            guildPanel.SetBool("isGuildPanelOpen", false);
        }
        else if (mainPanel.GetBool("isPanelOpen") == false && guildPanel.GetBool("isGuildPanelOpen") == false)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
