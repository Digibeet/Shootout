using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayerAnimator : PlayerAnimator
{
    public void Awake()
    {
        character_name = "soldier";
    }

    public override void Die()
    {
        playerAnimator.Play("Soldier_die");
        PlaySound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Soldier_draw");
        PlaySound(drawSound);
    }

    public override void EarlyShot()
    {
        
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Soldier_idle");
    }

    public override void Shoot()
    {
        playerAnimator.Play("Soldier_shoot", -1, 0);
        PlaySound(shotSound);
    }

    public void Fall()
    {
        PlaySound(fallSound);
    }
}
