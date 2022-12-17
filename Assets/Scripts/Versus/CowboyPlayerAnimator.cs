using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyPlayerAnimator : PlayerAnimator
{
    public override void Die()
    {
        playerAnimator.Play("Cowboy_die");
    }

    public override void Draw()
    {
        playerAnimator.Play("Cowboy_draw");
    }

    public override void Shoot()
    {
        playerAnimator.Play("Cowboy_shoot", -1, 0);
    }

    public override void EarlyShot()
    {
        playerAnimator.Play("Cowboy_earlyShot");
    }
}
