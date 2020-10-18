using UnityEngine;
using System.Collections;

public class PlayerDeadState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.playerAnimator.SetFloat("Speed", 0f);
        player.playerAnimator.SetBool("Attacking", false);
        player.playerAnimator.SetBool("Dead", true);
    }

    public override void ExitState(PlayerController player)
    {

    }

    public override void FixedUpdate(PlayerController player)
    {

    }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col)
    {

    }

    public override void Update(PlayerController player)
    {

    }
}
