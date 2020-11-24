using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class EnemyJumpIAWindow : MonoBehaviour
{
    private int count = 0;
    [MenuItem("My Tools/ Generate Jumper")]
    static void InstantiateJumper()
    {
        GameObject obj = GameObject.Find("Jumpers");
        
        if(obj == null)
            obj = new GameObject("Jumpers");
        GameObject jumper = new GameObject($"Jumper");
        jumper.transform.parent = obj.transform;
        jumper.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        
        jumper.AddComponent<EnemyJumpArea>();
        
        CircleCollider2D jumperCollider = jumper.AddComponent<CircleCollider2D>();
        jumperCollider.radius = .2f;
        
        jumper.tag = "Jumper";
    }
}
