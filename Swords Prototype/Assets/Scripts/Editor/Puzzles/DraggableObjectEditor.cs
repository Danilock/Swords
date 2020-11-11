using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
[CustomEditor(typeof(DraggableObject))]
public class DraggableObjectEditor : Editor
{
    private  SerializedProperty OnDragStarted, OnDragCompleted, OnDragCancelled;
    private SerializedProperty canInteract, returnOnDragCancelled, canCompleteDrag, completeTargetArea, changeTimeScale, timeScaleValue;
    private void OnEnable()
    {
        canInteract = serializedObject.FindProperty("canInteract");
        returnOnDragCancelled = serializedObject.FindProperty("returnOnDragCancelled");
        canCompleteDrag = serializedObject.FindProperty("canCompleteDrag");
        completeTargetArea = serializedObject.FindProperty("completeTargetArea");
        changeTimeScale = serializedObject.FindProperty("changeTimeScale");
        timeScaleValue = serializedObject.FindProperty("timeScaleValue");
        OnDragStarted = serializedObject.FindProperty("OnDragStarted");
        OnDragCompleted = serializedObject.FindProperty("OnDragCompleted");
        OnDragCancelled = serializedObject.FindProperty("OnDragCancelled");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(canInteract);
        EditorGUILayout.PropertyField(returnOnDragCancelled);
        EditorGUILayout.PropertyField(canCompleteDrag);
        EditorGUILayout.PropertyField(changeTimeScale);
        
        if (canCompleteDrag.boolValue == true)
        {
            EditorGUILayout.PropertyField(completeTargetArea);
        }
        
        if (returnOnDragCancelled.boolValue == false)
        {
            canCompleteDrag.boolValue = false;
        }

        if (changeTimeScale.boolValue == true)
        {
            EditorGUILayout.PropertyField(timeScaleValue);
        }
        
        //Drawing events
        EditorGUILayout.PropertyField(OnDragStarted);
        EditorGUILayout.PropertyField(OnDragCancelled);
        EditorGUILayout.PropertyField(OnDragCompleted);
        
        serializedObject.ApplyModifiedProperties();
    }
}
