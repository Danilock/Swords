using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    #region Attack Area
    [SerializeField] Vector2 attackAreaSize = new Vector2(2f, 2f);
    [SerializeField] Transform attackAreaPoint;
    #endregion
    #region Enemy and player scripts
    EnemyController enemy;
    PlayerController player;
    #endregion
    #region Cooldown and canAttack
    bool canAttack = true;
    float attackCooldown;
    #endregion
    [SerializeField] UnityEvent OnDetectPlayer;
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
        bool detectedPlayer = Physics2D.OverlapBox(attackAreaPoint.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        if (detectedPlayer && canAttack)
        {
            OnDetectPlayer.Invoke();
        }
    }

    public void DoDamage()
    {
        bool touchedPlayer = Physics2D.OverlapBox(attackAreaPoint.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        if (touchedPlayer)
        {
            player.TakeDamage(enemy.enemyProfile.baseDamage);
        }
    }

    public void PutAttackOnCooldown()
    {
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
