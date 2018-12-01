using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManagerScript : MonoBehaviour {

    GameObject SoundToggle;

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MuteSound()
    {
        if (AudioListener.pause)
        {
            SoundToggle.GetComponent<Toggle>().isOn = false;
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
            SoundToggle.GetComponent<Toggle>().isOn = true;
        }
    }

    // Use this for initialization
    void Start () {
        SoundToggle = GameObject.Find("ToggleSound");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
    }
}
