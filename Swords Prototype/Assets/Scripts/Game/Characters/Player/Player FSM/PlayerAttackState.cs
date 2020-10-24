using UnityEngine;
using System.Collections;

public class PlayerAttackState : PlayerBaseState
{
    float attackIndex;//Index to specify wich attack its gonna be called.
    bool entered;
    public override void EnterState(PlayerController player)
    {
        if (entered == true)
            return;
        entered = true;
        player.playerAnimator.SetBool("Attacking", true);

        if (attackIndex == 5)//If it's the last attack then the time will be in slow down mode.
            Time.timeScale = .3f;

        //Deciding wich attack animation it's gonna be called for next attack input.
        player.playerAnimator.SetFloat("AttackIndex", attackIndex);

        if (attackIndex == 5f)
            attackIndex = 0f;
        else
            attackIndex++;

        player.StopAllCoroutines();
        player.StartCoroutine(ResetAttackIndex());

    }

    public override void ExitState(PlayerController player) 
    {
        entered = false;
        player.playerAnimator.SetBool("Attacking", false);
        Time.timeScale = 1f;
    }
    public override void FixedUpdate(PlayerController player) { }

    public override void OnCollisionEnter2D(PlayerController player, Collision2D col) { }
    public override void Update(PlayerController player) { }

    /// <summary>
    /// Restart the attack index when the player is out of combat for 3 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetAttackIndex()
    {
        yield return new WaitForSeconds(3f);
        attackIndex = 0;
    }
}
