using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildPanelButtonHandler : MonoBehaviour {

    public Animator guildPanel;

    public bool isGuildPanelOpen = true;

    public void OpenGuildPanel()
    {
        guildPanel.SetBool("isGuildPanelOpen", true);
        isGuildPanelOpen = true;
        
    }

    public void CloseGuildPanel()
    {
        guildPanel.SetBool("isGuildPanelOpen", false);
        isGuildPanelOpen = false;
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
