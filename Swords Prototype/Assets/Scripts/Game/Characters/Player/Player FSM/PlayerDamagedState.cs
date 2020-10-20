using UnityEngine;
using System.Collections;

public class PlayerDamagedState : PlayerBaseState
{
    public override void EnterState(PlayerController player) 
    {
        player.playerAnimator.SetBool("Damaged", true);
        player.ch2D.enabled = false;//Desactivate the player controller script so the player can't move.
    }
    public override void ExitState(PlayerController player) 
    {
        player.playerAnimator.SetBool("Damaged", false);
        player.ch2D.enabled = true;
    }
    public override void FixedUpdate(PlayerController player) { }
    public override void OnCollisionEnter2D(PlayerController player, Collision2D col) { }

    public override void Update(PlayerController player) { }
}
