using UnityEngine;
using System.Collections;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(EnemyController enemy)
    {

    }

    public override void ExitState(EnemyController enemy)
    {

    }

    public override void Update(EnemyController enemy)
    {
        Debug.Log($"{enemy.gameObject.name} is dead");
    }
}
