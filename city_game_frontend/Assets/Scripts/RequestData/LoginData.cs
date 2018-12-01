﻿using System.Collections;
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
        string path = "Assets/Scripts/RequestData/loginData.txt";
        StreamReader reader = new StreamReader(path);

        try { 
        string login = reader.ReadLine();
        string pass = reader.ReadLine();

            this.login = login;
            this.pass = pass;
        }
        catch (System.Exception e) {
            Debug.LogError("WILL NOT LOG IN UNLESS YOU MAKE A DEFAULT LOGIN DATA FILE");
        }
    }
}
