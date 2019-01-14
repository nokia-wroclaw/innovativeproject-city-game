using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class LoginData
{
    public int type = Const.MESSAGE_TYPE_AUTH_EVENT;
    public string login; //= "wint3rmute";
    public string pass;//= "baczekbezraczek";

    // TODO
    public LoginData()
    {
        
        if (Application.platform == RuntimePlatform.Android)
        {
            if (PlayerPrefs.HasKey("Login")){
                this.login = UIManagerScript.constLoginKey;
            }
            if (PlayerPrefs.HasKey("Password"))
            {
                this.pass = UIManagerScript.constPasswordKey;
            }

            return;
        }

            // Using Pawel's account as a 'base' one
            this.login = "pawel";
            this.pass = "jaktamsprzeglo";

    }
}
