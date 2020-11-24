using UnityEngine;
using System.Collections;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.playerAnimator.SetBool("Jump", true);
        player.ch2D.CrouchSpeed /= 2;
    }

    public override void ExitState(PlayerController player)
    {
        player.ch2D.CrouchSpeed *= 2;
        player.ch2D.AirControl = true;
        player.playerAnimator.SetBool("Jump", false);
    }

    public override void Update(PlayerController player)
    {
        player.PlayerInput();
        if (player.ch2D.m_Grounded)
        {
            player.SetState(player.idleState);
        }

        //Don't let player continuing doin control air if detects a wall.
        if (player.CollidedWall())
        {
            player.ch2D.AirControl = false;
        }
    }

    public override void FixedUpdate(PlayerController player)
    {
        player.ch2D.Move(player.horizontalMove, false, false);
    }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col)
    {
        
    }
}
