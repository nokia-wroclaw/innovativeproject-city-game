using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

    public Text loginText;
    public Text passwordText;
    public string constLoginKey, constPasswordKey;
    public GameObject loginPanel;
    //public Button loginButton;

    public void Login()
    {
        if (loginText == null && passwordText == null)
            Debug.Log("Login and password needed!");
        else
        {
            constLoginKey = loginText.text;
            constPasswordKey = passwordText.text;
            PlayerPrefs.SetString("Login", constLoginKey);
            PlayerPrefs.SetString("Password", constPasswordKey);
        }
    }

    public void ExitLoginPanel()
    {
        loginPanel.SetActive(false);
        //if(PlayerPrefs.HasKey("Login"))
            Debug.Log(PlayerPrefs.GetString("Login"));
        //Debug.Log(PlayerPrefs.GetString("Password"));
    }


    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("ChunksScene");
    }

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
    }
}
