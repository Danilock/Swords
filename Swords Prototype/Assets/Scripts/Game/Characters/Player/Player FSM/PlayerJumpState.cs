using UnityEngine;
using System.Collections;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    public override void Update(PlayerController player)
    {
        player.PlayerInput();
    }

    public override void FixedUpdate(PlayerController player)
    {
        player.ch2D.Move(player.horizontalMove, false, false);
    }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col)
    {
        if (player.rgb2D.velocity.y <= 0.1f && player.ch2D.m_Grounded)
        {
            player.SetState(player.idleState);
        }
    }
}
