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
        throw new System.NotImplementedException();
    }

    public override void Draw()
    {
        playerAnimator.Play("Soldier_draw");
    }

    public override void EarlyShot()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        playerAnimator.Play("Soldier_shoot", -1, 0);
    }
}
