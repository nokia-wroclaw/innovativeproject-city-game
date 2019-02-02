using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string name;

    public int level;
    public int exp;
    public float Cementia;
    public float Plasmatia;
    public float Auferia;

    public string guild;

    public List<GuildInvite> invites;
}
