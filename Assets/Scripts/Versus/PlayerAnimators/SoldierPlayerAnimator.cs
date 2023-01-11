using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayerAnimator : PlayerAnimator
{
    [SerializeField] protected AudioClip fallSound;
    public void Awake()
    {
        character_name = "soldier";
    }

    public override void Die()
    {
        playerAnimator.Play("Soldier_die");
        playSound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Soldier_draw");
        playSound(drawSound);
    }

    public override void EarlyShot()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Soldier_idle");
    }

    public override void Shoot()
    {
        playerAnimator.Play("Soldier_shoot", -1, 0);
        playSound(shotSound);
    }

    public void Fall()
    {
        playSound(fallSound);
    }
}
