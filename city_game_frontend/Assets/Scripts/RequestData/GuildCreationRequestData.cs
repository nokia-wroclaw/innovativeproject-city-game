using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuildCreationRequestData
{
    public int type = Const.MESSAGE_TYPE_CREATE_GUILD;
    public string guild_name;

    public GuildCreationRequestData(string guild_name)
    {
        this.guild_name = guild_name;
    }
}
