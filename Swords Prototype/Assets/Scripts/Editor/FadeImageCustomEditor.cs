using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(FadeImage))]
public class FadeImageCustomEditor : Editor
{
    SerializedProperty imageToFade, canvasToFade, target, fadeSpeed, fadeMode, onFadeShow, onFadeHide, changeColorsOnEditor;
    private void OnEnable()
    {
        imageToFade = serializedObject.FindProperty("imageToFade");
        canvasToFade = serializedObject.FindProperty("canvasToFade");
        target = serializedObject.FindProperty("target");
        fadeMode = serializedObject.FindProperty("fadeMode");
        fadeSpeed = serializedObject.FindProperty("fadeSpeed");
        onFadeShow = serializedObject.FindProperty("onFadeShow");
        onFadeHide = serializedObject.FindProperty("onFadeHide");
        changeColorsOnEditor = serializedObject.FindProperty("changeColorsOnEditor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(imageToFade);
        EditorGUILayout.PropertyField(canvasToFade);
        EditorGUILayout.PropertyField(target);
        EditorGUILayout.PropertyField(fadeMode);
        EditorGUILayout.PropertyField(fadeSpeed);

        switch (fadeMode.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(onFadeShow);
                if(changeColorsOnEditor.boolValue == true)
                    ImageAlphaColor(true);
                break;
            case 1:
                EditorGUILayout.PropertyField(onFadeHide);
                if(changeColorsOnEditor.boolValue == true)
                    ImageAlphaColor(false);
                break;
        }


        EditorGUILayout.PropertyField(changeColorsOnEditor);
        serializedObject.ApplyModifiedProperties();
    }

    void ImageAlphaColor(bool showOrHide)
    {
        if (target.enumValueIndex == 0)
        {
            if (imageToFade.objectReferenceValue == null)
                return;
            var currentImage = (Image)imageToFade.objectReferenceValue;

            Color currentColor = currentImage.color;
            currentColor.a = showOrHide ? 0 : 1;

            currentImage.color = currentColor;
        }
        else if(target.enumValueIndex == 1)
        {
            if (canvasToFade.objectReferenceValue == null)
                return;
            var currentCanvas = (CanvasGroup) canvasToFade.objectReferenceValue;

            float currentColor = currentCanvas.alpha;
            currentColor = showOrHide ? 0 : 1;

            currentCanvas.alpha = currentColor;
        }
    }
}
