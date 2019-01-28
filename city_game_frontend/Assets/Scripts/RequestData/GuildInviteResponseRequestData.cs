using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuildInviteResponseRequestData
{
    public int type = Const.MESSAGE_TYPE_RESPOND_TO_GUILD_INVITE;
    public int answer;
    public int invite_id;

    public GuildInviteResponseRequestData(int answer, int invite_id)
    {
        this.answer = answer;
        this.invite_id = invite_id;
    }
}
