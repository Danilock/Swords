using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] Transform attackAreaPosition;
    [SerializeField] Vector2 attackAreaSize;

    /// <summary>
    /// Creates a overlapBox area and hits every enemy detected by certain damage.
    /// </summary>
    /// <param name="damage"></param>
    public void MeleeAttack(float damage)
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
