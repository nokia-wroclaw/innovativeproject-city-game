using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuildKickRequestData
{
    public int type = Const.MESSAGE_TYPE_SEND_GUILD_KICK_REQUEST;
    public string nick_to_kick;

    public GuildKickRequestData(string nick_to_kick)
    {
        this.nick_to_kick = nick_to_kick;
    }
}
