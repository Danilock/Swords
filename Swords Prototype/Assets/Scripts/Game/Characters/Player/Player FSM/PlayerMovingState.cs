using UnityEngine;
using System.Collections;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    public override void Update(PlayerController player)
    {
        player.horizontalMove = Input.GetAxisRaw("Horizontal");
        player.ch2D.Move(player.horizontalMove, false, false);

        if (player.rgb2D.velocity.magnitude <= 0.1f)
        {
            player.SetState(player.idleState);
        }
    }
}
