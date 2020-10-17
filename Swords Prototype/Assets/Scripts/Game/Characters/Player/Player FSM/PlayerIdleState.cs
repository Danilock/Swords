﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        //TODO: Idle Animation by animator controller.
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    public override void Update(PlayerController player)
    {
        player.PlayerInput();

        if (Input.GetButtonDown("Attack"))
        {
            player.attackController.Attack();
            player.SetState(player.attackState);
        }
    }

    public override void FixedUpdate(PlayerController player)
    {
        player.ch2D.Move(player.horizontalMove, false, false);

        //Detecting if player's velocity is greater than 0.01 in X axis to set the moving state
        if (player.rgb2D.velocity.magnitude > 0.1f)
        {
            player.SetState(player.movingState);
        }
    }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col)
    {
        
    }
}
