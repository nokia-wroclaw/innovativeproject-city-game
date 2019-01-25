using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof (GuildActions))]
public class GuildActionsExtensions: Editor
{
    int structureToTakeoverID = 0;
    string guildName = "";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();

        // target is a derived field
        GuildActions targetScript = (GuildActions)target;

      

        if (GUILayout.Button("Get Guild Data"))
        {

            targetScript.getGuildData();

        }

    }
}

#endif