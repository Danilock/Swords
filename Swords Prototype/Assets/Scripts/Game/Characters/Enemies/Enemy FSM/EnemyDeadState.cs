using UnityEngine;
using System.Collections;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.enemyAnimator.SetBool("Attack", false);
        enemy.enemyAnimator.SetBool("Walking", false);
        enemy.enemyAnimator.SetBool("Dead", true);
    }

    public override void ExitState(EnemyController enemy)
    {

    }

    public override void Update(EnemyController enemy)
    {
        Debug.Log($"{enemy.gameObject.name} is dead");
    }
}
