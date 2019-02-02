using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

[System.Serializable]
public class GuildData{
    public new string name;

    public int members_count;
    public List<string> members;

    public GuildData()
    {

    }
}
