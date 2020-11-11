﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(InteractableObject))]
public class InteractiveObjectEditor : Editor
{
    SerializedProperty canBeReactivatedField, timeToReactivateField, OnInteractWithPlayerField, canInteract, interacting;
    private void OnEnable()
    {
        canBeReactivatedField = serializedObject.FindProperty("canBeReactivated");
        timeToReactivateField = serializedObject.FindProperty("timeToReactivate");
        OnInteractWithPlayerField = serializedObject.FindProperty("OnInteractWithPlayer");
        canInteract = serializedObject.FindProperty("canInteract");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(canBeReactivatedField);
        EditorGUILayout.PropertyField(canInteract);
        EditorGUILayout.PropertyField(OnInteractWithPlayerField);
        if(canBeReactivatedField.boolValue == true)
        {
            EditorGUILayout.PropertyField(timeToReactivateField);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
