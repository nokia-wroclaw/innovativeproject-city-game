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
                this.login = PlayerPrefs.GetString("Login");
            }
            if (PlayerPrefs.HasKey("Password"))
            {
                this.pass = PlayerPrefs.GetString("Password");
            }

            return;
        }

        
        try {

        string path = "Assets/Scripts/RequestData/loginData.txt";
        StreamReader reader = new StreamReader(path);


        string login = reader.ReadLine();
        string pass = reader.ReadLine();

            this.login = login;
            this.pass = pass;
        }
        catch (System.Exception e) {

            // Using Pawel's account as a 'base' one
            this.login = "pawel";
            this.pass = "jaktamsprzeglo";

        }
    }
}
