using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Content;

[CustomEditor(typeof(Trap))]
public class TrapEditor : Editor
{
    SerializedProperty hasAnimation, destroyWhenDesactivate, canPushPlayer, trapDamage, force, inParent, parent;
    private void OnEnable()
    {
        hasAnimation = serializedObject.FindProperty("hasAnimation");
        destroyWhenDesactivate = serializedObject.FindProperty("destroyWhenDesactivate");
        canPushPlayer = serializedObject.FindProperty("canPushPlayer");
        force = serializedObject.FindProperty("force");
        trapDamage = serializedObject.FindProperty("trapDamage");
        inParent = serializedObject.FindProperty("inParent");
        parent = serializedObject.FindProperty("parent");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(trapDamage);
        EditorGUILayout.PropertyField(canPushPlayer);
        EditorGUILayout.PropertyField(hasAnimation);

        if (hasAnimation.boolValue == false)
        {
            AddOrRemoveAnimator(false);
            EditorGUILayout.PropertyField(destroyWhenDesactivate);
        }
        else
        {
            EditorGUILayout.PropertyField(inParent);
            AddOrRemoveAnimator(true);
        }

        if(canPushPlayer.boolValue == true)
        {
            EditorGUILayout.PropertyField(force);
        }
        if (inParent.boolValue == true)
        {
            EditorGUILayout.PropertyField(parent);
        }

        serializedObject.ApplyModifiedProperties();
    }

    void AddOrRemoveAnimator(bool add)
    {
        var t = (target as Trap);
        if (add)
        {
            t.gameObject.AddComponent<Animator>();
        }
        else
        {
            DestroyImmediate(t.GetComponent<Animator>());
        }
    }
}
