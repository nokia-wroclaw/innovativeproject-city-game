using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersBarHandler : MonoBehaviour {

    public Image expBar, healthBar;

    //arguments from 0 to 100
    public void gainExp(float percents)
    {
        expBar.fillAmount = percents/100;
    }

    //arguments from 0 to 100
    public void setHPBar(float percents)
    {
        healthBar.fillAmount = percents/100;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
