using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Hey!, I'm Idle");
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    public override void Update(PlayerController player)
    {
        player.horizontalMove = Input.GetAxisRaw("Horizontal");
        player.ch2D.Move(player.horizontalMove, false, false);

        //Detecting if player's velocity is greater than 0.01 in X axis to set the moving state
        if(player.rgb2D.velocity.magnitude > 0.1f)
        {
            player.SetState(player.movingState);
        }
    }
}
