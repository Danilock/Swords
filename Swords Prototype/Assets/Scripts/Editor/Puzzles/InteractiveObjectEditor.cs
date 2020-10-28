using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(InteractableObject))]
public class InteractiveObjectEditor : Editor
{
    SerializedProperty canBeReactivatedField, timeToReactivateField, OnInteractWithPlayerField;
    private void OnEnable()
    {
        canBeReactivatedField = serializedObject.FindProperty("canBeReactivated");
        timeToReactivateField = serializedObject.FindProperty("timeToReactivate");
        OnInteractWithPlayerField = serializedObject.FindProperty("OnInteractWithPlayer");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(canBeReactivatedField);

        if(canBeReactivatedField.boolValue == true)
        {
            EditorGUILayout.PropertyField(timeToReactivateField);
        }

        EditorGUILayout.PropertyField(OnInteractWithPlayerField);


        serializedObject.ApplyModifiedProperties();
    }
}
