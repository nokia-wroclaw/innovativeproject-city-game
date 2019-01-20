using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsPanelHandler : MonoBehaviour {

    public GameObject notificationPanel;
    public Text text;

    public void showNotification(string notificationText)
    {
        notificationPanel.SetActive(true);
        text.text = notificationText;
        Invoke("notificationOff", 3);
    }

    public void showNotification(string notificationText,int seconds)
    {
        notificationPanel.SetActive(true);
        text.text = notificationText;
        Invoke("notificationOff", seconds);
    }

    public void notificationOff()
    {
        notificationPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        notificationPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
