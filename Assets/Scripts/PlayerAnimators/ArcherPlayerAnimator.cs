using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPlayerAnimator : PlayerAnimator
{
    public void Awake()
    {
        character_name = "archer";
    }
    public override void Die()
    {
        playerAnimator.Play("Archer_die");
        PlaySound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Archer_draw");
        PlaySound(drawSound);
    }

    public override void EarlyShot()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        playerAnimator.Play("Archer_shoot");
        PlaySound(shotSound);
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Archer_idle");
    }
}
