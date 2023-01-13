using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffPlayerAnimator : PlayerAnimator
{
    public override void Die()
    {
        playerAnimator.Play("Sheriff_die");
        playSound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Sheriff_draw");
        playSound(drawSound);
    }

    public override void EarlyShot()
    {
        
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Sheriff_idle");
    }

    public override void Shoot()
    {
        playerAnimator.Play("Sheriff_shoot", -1, 0);
        playSound(shotSound);
    }
}
