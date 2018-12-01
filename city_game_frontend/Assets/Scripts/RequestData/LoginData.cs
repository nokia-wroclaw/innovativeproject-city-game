using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginData
{
    public int type = Const.MESSAGE_TYPE_AUTH_EVENT;
    public string login = "wint3rmute";
    public string pass = "baczekbezraczek";

    // TODO
    public LoginData()
    {

    }
}
