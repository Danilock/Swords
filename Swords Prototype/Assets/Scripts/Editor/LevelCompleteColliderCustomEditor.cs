using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelLoader))]
public class LevelCompleteColliderCustomEditor : Editor
{
    SerializedProperty nextLevel, sceneSelected;
    List<string> scenes = new List<string>();

    private void OnEnable()
    {
        nextLevel = serializedObject.FindProperty("nextLevel");
        sceneSelected = serializedObject.FindProperty("sceneSelected");

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            scenes.Add(scene.path);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        sceneSelected.intValue = EditorGUILayout.Popup("Scene To Load", sceneSelected.intValue, scenes.ToArray());
        nextLevel.stringValue = scenes[sceneSelected.intValue];

        serializedObject.ApplyModifiedProperties();
    }
}
