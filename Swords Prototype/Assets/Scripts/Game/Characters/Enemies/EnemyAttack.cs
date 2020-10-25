using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Vector2 attackAreaSize = new Vector2(2f, 2f);
    [SerializeField] Transform attackAreaPoint;
    [SerializeField] UnityEvent OnDetectPlayer;
    bool canAttack = true;
    float attackCooldown;
    EnemyController enemy;
    PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = GetComponent<EnemyController>();
        attackCooldown = enemy.enemyProfile.attackCooldown;
    }

    /// <summary>
    /// Draws the attack area and if detects the player then starts the attack animation.
    /// </summary>
    public void DrawAttackArea()
    {
        bool touchedPlayer = Physics2D.OverlapBox(attackAreaPoint.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        if (touchedPlayer)
        {
            OnDetectPlayer.Invoke();
        }
    }

    public void DoDamage()
    {
        if (!canAttack)
            return;
        bool touchedPlayer = Physics2D.OverlapBox(attackAreaPoint.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        if (touchedPlayer)
        {
            player.TakeDamage(enemy.enemyProfile.baseDamage);
        }
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        if (attackAreaPoint == null)
            return;
        Gizmos.color = Color.red * .5f;
        Gizmos.DrawCube(attackAreaPoint.position, attackAreaSize);
    }
}
