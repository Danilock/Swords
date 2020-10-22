using UnityEngine;
using System.Collections;

public class PlayerBowAttackState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.playerAnimator.SetBool("Bow Attack", true);
    }

    public override void ExitState(PlayerController player)
    {
        player.playerAnimator.SetBool("Bow Attack", false);
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
