﻿using UnityEngine;
using System.Collections;

public class EnemyTakingDamageState : EnemyBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        
    }

    public override void ExitState(EnemyController enemy)
    {
        Debug.Log("Exiting from Damaged state");
    }

    public override void Update(EnemyController enemy)
    {

    }
}