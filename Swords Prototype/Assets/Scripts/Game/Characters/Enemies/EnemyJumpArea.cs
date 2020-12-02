using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class EnemyJumpArea : MonoBehaviour
{
    private EnemyIA IAfromActualEnemy;
    private Rigidbody2D rgbFromActualEnemy;
    private CircleCollider2D circleCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IAfromActualEnemy = other.GetComponent<EnemyIA>();

            if (IAfromActualEnemy != null && IAfromActualEnemy.canJump)
            {
                IAfromActualEnemy.Jump();
            }
        }
    }

    private void OnDrawGizmos()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        
        Gizmos.color = Color.green * 0.5f;
        Gizmos.DrawSphere(transform.position + new Vector3(circleCollider.offset.x, circleCollider.offset.y ,0f), circleCollider.radius);

    #if UNITY_EDITOR	
        Handles.Label(transform.position, "Jumper:" + gameObject.name);
    #endif
    }
}
