﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] Transform attackAreaPosition;
    [SerializeField] Vector2 attackAreaSize;
    [SerializeField] float damage;
    int attackIndex;
    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackAreaPosition.position, attackAreaSize, 0f, LayerMask.GetMask("Enemy"));
    
        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (attackAreaPosition == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(attackAreaPosition.position, attackAreaSize);
    }
}