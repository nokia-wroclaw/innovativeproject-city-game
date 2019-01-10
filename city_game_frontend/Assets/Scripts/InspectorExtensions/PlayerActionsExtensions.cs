using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof (PlayerActions))]
public class PlayerActionsExtensions: Editor
{
    int structureToTakeoverID = 0;
    string guildName = "";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();

        // target is a derived field
        PlayerActions targetScript = (PlayerActions)target;

        //structureToTakeoverID = int.Parse( GUILayout.TextField("1"));

        GUILayout.BeginHorizontal();
        GUILayout.Label("Structure ID");

        // This looks pretty clunky, but that's the only way I managed to got it to work
        structureToTakeoverID = EditorGUILayout.IntField(structureToTakeoverID);

        if (GUILayout.Button("Take over"))
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Game not started!");
                return;
            }


            targetScript.sendStructureTakeoverRequest(structureToTakeoverID);
        }

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

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Enter Building Mode"))
        {
            targetScript.enterBuildingMode();

        }

        if (GUILayout.Button("Confirm Building"))
        {
            placeBuilding.Instance.confirmBuildingPlacement();
            targetScript.leaveBuildingMode();

        }


        if (GUILayout.Button("Cancel Building"))
        {

            targetScript.leaveBuildingMode();

        }


    }
}

#endif