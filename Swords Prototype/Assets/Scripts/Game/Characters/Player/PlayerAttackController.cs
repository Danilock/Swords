using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] Transform attackAreaPosition;
    [SerializeField] Vector2 attackAreaSize;
    PlayerController player;
    float attackIndex = 0f;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void Attack(float damage)
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackAreaPosition.position, attackAreaSize, 0f, LayerMask.GetMask("Enemy"));
    
        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(damage);
        }

        player.playerAnimator.SetFloat("AttackIndex", 0);
    }

    private void OnDrawGizmos()
    {
        if (attackAreaPosition == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(attackAreaPosition.position, attackAreaSize);
    }
}
