using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class LoginData
{
    public int type = Const.MESSAGE_TYPE_AUTH_EVENT;
    public string login;
    public string pass;

    // TODO
    public LoginData()
    {
      
        if (PlayerPrefs.HasKey(UIManagerScript.CONST_LOGIN_KEY) && PlayerPrefs.HasKey(UIManagerScript.CONST_PASSWORD_KEY))
        {
            
            this.login = PlayerPrefs.GetString(UIManagerScript.CONST_LOGIN_KEY);
            this.pass = PlayerPrefs.GetString(UIManagerScript.CONST_PASSWORD_KEY);

            return;
        }

        // Using Pawel's account as a 'default' one
        this.login = "gracz";
        this.pass = "baczekbezraczek";

    }
}
