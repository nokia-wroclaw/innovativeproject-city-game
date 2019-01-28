using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof (GuildActions))]
public class GuildActionsExtensions: Editor
{
    string invitationReceiver = "";
    string guildName = "";
    int invite_id = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();

        // target is a derived field
        GuildActions targetScript = (GuildActions)target;

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Create Guild");
        guildName = EditorGUILayout.TextField(guildName);

        if (GUILayout.Button("Create Guild"))
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Game not started!");
                return;
            }

            guildName = EditorGUILayout.TextField(guildName);
            Debug.Log("Creating " + guildName);

            targetScript.sendCreateGuildRequest(guildName);
        }

        GUILayout.EndHorizontal();


        if (GUILayout.Button("Get Guild Data"))
        {

            targetScript.getGuildData();

        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Send guild invite");
        invitationReceiver = EditorGUILayout.TextField(invitationReceiver);

        if (GUILayout.Button("Send"))
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Game not started!");
                return;
            }

            targetScript.inviteToGuild(invitationReceiver);
         }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Invite ID");
        invite_id = EditorGUILayout.IntField(invite_id);

        if (GUILayout.Button("Accept"))
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Game not started!");
                return;
            }

            targetScript.acceptInvite(invite_id);
        }

        if (GUILayout.Button("Reject"))
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Game not started!");
                return;
            }

            targetScript.rejectInvite(invite_id);
        }

    }
}

#endif