using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuildInviteSendRequestData
{
    public int type = Const.MESSAGE_TYPE_SEND_GUILD_INVITE;
    public string receiver;

    public GuildInviteSendRequestData(string receiver)
    {
        this.receiver = receiver;
    }
}