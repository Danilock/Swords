using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDraggableObject : EditorWindow
{
    private Sprite draggableObjectImage;
    private enum ColliderType{CircleCollider, BoxCollider, PolygonCollider, CapsuleCollider}

    private ColliderType colliderType;
    private bool hasPlatformEffector;
    private float surfaceArc = 180;
    
    [MenuItem("My Tools/ Create Dragable Object")]
    static void Init()
    {
        GenerateDraggableObject window =
            (GenerateDraggableObject)GetWindow(typeof(GenerateDraggableObject));
        window.position = new Rect(100f, 100f, 500f, 150f);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Draggable Object", EditorStyles.label);

        draggableObjectImage = (Sprite) EditorGUILayout.ObjectField("Sprite", draggableObjectImage, typeof(Sprite), false);
        colliderType = (ColliderType) EditorGUILayout.EnumPopup("Collider Type", colliderType);
        hasPlatformEffector = EditorGUILayout.Toggle("Add Platform Effector", hasPlatformEffector);
        
        if (hasPlatformEffector)
        {
            surfaceArc = EditorGUILayout.FloatField("Surface Arc", surfaceArc);
        }
        
        if (GUILayout.Button("Generate Draggale Object"))
        {
            GenerateDraggable();
        }
    }

    void GenerateCollider(ColliderType col, GameObject draggableObject)
    {
        switch (col)
        {
            case ColliderType.BoxCollider:
                draggableObject.AddComponent<BoxCollider2D>();
                break;
            case  ColliderType.CapsuleCollider:
                draggableObject.AddComponent<CapsuleCollider2D>();
                break;
            case  ColliderType.CircleCollider:
                draggableObject.AddComponent<CircleCollider2D>();
                break;
            case  ColliderType.PolygonCollider:
                draggableObject.AddComponent<PolygonCollider2D>();
                break;
                
        }
    }

    void GenerateDraggable()
    {
        if (draggableObjectImage == null)
        {
            GetWindow(typeof(GenerateDraggableObject)).ShowNotification(new GUIContent("Image is null"));
            return;
        }

        #region Finding Draggables Parent
        GameObject parentOfDraggables = GameObject.Find("Draggables");
        if(parentOfDraggables == null)
            parentOfDraggables = new GameObject("Draggables");
        #endregion
        GameObject draggableObject = new GameObject("New Draggable Object");
        draggableObject.tag = "Draggable Object";
        draggableObject.transform.parent = parentOfDraggables.transform;
        
        SpriteRenderer sprite = draggableObject.AddComponent<SpriteRenderer>();
        sprite.sprite = draggableObjectImage;
        sprite.sortingLayerName = "Environment";
            
        //Generating collider type.
        GenerateCollider(colliderType, draggableObject);
            
        draggableObject.AddComponent<LerpMovement>();
        draggableObject.AddComponent<DraggableObject>();

        if (hasPlatformEffector)
        {
            PlatformEffector2D effector = draggableObject.AddComponent<PlatformEffector2D>();
            draggableObject.GetComponent<Collider2D>().usedByEffector = true;
            effector.surfaceArc = surfaceArc;
        }
    }
}
