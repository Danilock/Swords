using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
    public abstract void EnterState (EnemyController enemy);
    public abstract void ExitState(EnemyController enemy);
    public abstract void Update(EnemyController enemy);
}
