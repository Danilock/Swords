using UnityEngine;
using System.Collections;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.enemyAnimator.SetBool("Attack", true);
    }

    public override void ExitState(EnemyController enemy)
    {
        enemy.enemyAnimator.SetBool("Attack", false);
    }

    public override void Update(EnemyController enemy)
    {
        
    }
}
