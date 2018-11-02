using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuContainer : MonoBehaviour {

    public CanvasGroup mainScreen, signinScreen;

    public Button Exit, SignIn, backMain;
    // Use this for initialization
    void Start ()
    {
        hideAll();
        mainScreen.alpha = 1;


        Exit.onClick.AddListener(() => exitPressed());

        backMain.onClick.AddListener(() => switchMain());

        SignIn.onClick.AddListener(() =>
        {
            hideAll();
            signinScreen.alpha = 1;
        });
    }
	


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector2 pos = Camera.main.transform.position;
            pos.x += -10;
            Camera.main.transform.position = pos;
        }
    }

    void switchMain()
    {
        hideAll();
        mainScreen.alpha = 1;
    }

    void exitPressed()
    {
        Debug.Log("exit");
    }

    void hideAll()
    {
        mainScreen.alpha = 0;
        signinScreen.alpha = 0;
    }
}
