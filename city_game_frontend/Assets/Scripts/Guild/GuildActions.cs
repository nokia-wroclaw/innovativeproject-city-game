using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class GuildActions : MonoBehaviour {

    public static GuildActions Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void getGuildData()
    {
        ServerSocket.Instance.send(this.gameObject, JsonUtility.ToJson(new GuildDataRequestData()), guildDataCallback);
    }

    public Request.callbackFunc guildDataCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {

        try { 
            GuildDataManager.Instance.guildData = JsonUtility.FromJson<GuildData>(data);
        }
        catch (System.Exception e) {
            Debug.LogError("Cannot parse!");
            Debug.LogError(e);
        }
    });

    public void sendCreateGuildRequest(string guildName)
    {
        ServerSocket.Instance.send(
            gameObject,
            JsonUtility.ToJson(
                new GuildCreationRequestData(guildName)
                ), guildCreationCallback);
    }

    public Request.callbackFunc guildCreationCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Guild created: " + data);
    });


    public void inviteToGuild(string nick)
    {
        ServerSocket.Instance.send(
            gameObject,
            JsonUtility.ToJson(
                new GuildInviteSendRequestData(
                    nick
                    )),
                guildInvitationSendingCallback);

    }

    public Request.callbackFunc guildInvitationSendingCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Invite status: " + data);
    });

    public void acceptInvite(int invite_id)
    {
        answerInvite(Const.GUILD_INVITE_ACCEPT, invite_id);
    }

    public void rejectInvite(int invite_id)
    {
        answerInvite(Const.GUILD_INVITE_DENY, invite_id);
    }

    
    public void answerInvite(int answer, int invite_id)
    {
        ServerSocket.Instance.send(
            gameObject,
            JsonUtility.ToJson(

                new GuildInviteResponseRequestData(
                    answer, invite_id
                    )
                ), guildInvitationAnswerCallback);
    }

    public void sendKickRequest(string player_nick)
    {
        ServerSocket.Instance.send(
           gameObject,
           JsonUtility.ToJson(

               new GuildKickRequestData(
                   player_nick
                   )
               ), guildKickRequestCallback);
    }

    public Request.callbackFunc guildInvitationAnswerCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Invite status: " + data);
        GuildActions.Instance.getGuildData();
    });

    public Request.callbackFunc guildKickRequestCallback = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log("Kick request status: " + data);
        GuildActions.Instance.getGuildData();
    });

}
