using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Vector2 attackAreaSize = new Vector2(2f, 2f);
    [SerializeField] Transform attackAreaPoint;
    EnemyController enemy;
    PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = GetComponent<EnemyController>();
    }

    /// <summary>
    /// Draws the attack area and if detects the player then starts the attack animation.
    /// </summary>
    public void DrawAttackArea()
    {

    }

    public void DoDamage()
    {
        bool touchedPlayer = Physics2D.OverlapBox(attackAreaPoint.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        if (touchedPlayer)
        {
            player.TakeDamage(enemy.enemyProfile.baseDamage);
        }
    }
}
