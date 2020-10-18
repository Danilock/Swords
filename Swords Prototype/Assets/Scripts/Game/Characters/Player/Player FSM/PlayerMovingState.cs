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
        player.PlayerInput();

        if (Input.GetButtonDown("Attack"))
        {
            player.SetState(player.attackState);
        }
    }

    public override void FixedUpdate(PlayerController player)
    {
        player.ch2D.Move(player.horizontalMove, false, false);
        player.playerAnimator.SetFloat("Speed", Mathf.Abs(player.horizontalMove));

        if (player.rgb2D.velocity.magnitude <= 0.1f)
        {
            player.SetState(player.idleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col)
    {
        
    }
}
