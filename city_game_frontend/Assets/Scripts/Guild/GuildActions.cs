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
        Debug.Log(data);

        try { 
            GuildData retrievedGuildData = JsonUtility.FromJson<GuildData>(data);
            Debug.Log(retrievedGuildData.name);

            Debug.Log(retrievedGuildData.members[0]);
            Debug.Log(retrievedGuildData.members[1]);

        }
        catch (System.Exception e)
        {
            Debug.LogError("Cannot parse!");
            Debug.LogError(e);
        }
         });


    public void inviteToGuild(string nick)
    {

    }

    public void kickFromGuild(string nick)
    {

    }


}
