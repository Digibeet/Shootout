using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePlayerAnimator : PlayerAnimator
{
    public override void Die()
    {
        PlaySound(dieSound);
        playerAnimator.Play("Rogue_die");
    }

    public override void Draw()
    {
        PlaySound(drawSound);
        playerAnimator.Play("Rogue_draw");
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Rogue_idle");
    }

    public override void Shoot()
    {
        playerAnimator.Play("Rogue_shoot", -1, 0);
        PlaySound(shotSound);
    }

    public override void EmptyClip()
    {
        
    }
}
