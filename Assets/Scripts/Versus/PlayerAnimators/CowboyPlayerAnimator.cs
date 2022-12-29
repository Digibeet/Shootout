using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyPlayerAnimator : PlayerAnimator
{
    public override void Die()
    {
        playerAnimator.Play("Cowboy_die");
        playSound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Cowboy_draw");
        playSound(drawSound);
    }

    public override void Shoot()
    {
        playerAnimator.Play("Cowboy_shoot", -1, 0);
        playSound(shotSound);
    }

    public override void EarlyShot()
    {
        playerAnimator.Play("Cowboy_earlyShot");
    }
}
