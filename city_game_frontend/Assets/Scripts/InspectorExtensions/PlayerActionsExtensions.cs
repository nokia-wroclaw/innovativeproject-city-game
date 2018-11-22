using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof (PlayerActions))]
public class PlayerActionsExtensions: Editor
{
    int structureToTakeoverID = 0;

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

        if(GUILayout.Button("Take over"))
        {
            if(!Application.isPlaying)
            {
                Debug.Log("Game not started!");
                return;
            }


            targetScript.sendStructureTakeoverRequest(structureToTakeoverID);
        }


    }
}

#endif