﻿using UnityEngine;
using System.Collections;

public class PlayerAttackState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.playerAnimator.SetBool("Attacking", true);
    }

    public override void ExitState(PlayerController player) 
    { 

    }
    public override void FixedUpdate(PlayerController player) { }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col) { }
    public override void Update(PlayerController player) { }
}
