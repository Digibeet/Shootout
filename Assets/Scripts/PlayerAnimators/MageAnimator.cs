using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAnimator : PlayerAnimator
{
    public void Awake()
    {
        character_name = "Mage";
    }

    public override void Die()
    {
        playerAnimator.Play("Mage_die");
        PlaySound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Mage_draw");
        PlaySound(drawSound);
    }

    public override void Shoot()
    {
        playerAnimator.Play("Mage_shoot", -1, 0);
        PlaySound(shotSound);
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Mage_idle", -1, 0);
    }
}
