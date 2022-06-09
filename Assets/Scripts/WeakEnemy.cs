using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : Enemy
{
    // POLYMORPHISM
    public override void InstantiateEnemy(GameManager new_gameManager)
    {
        shootTime = 3.0f;
        base.InstantiateEnemy(new_gameManager);
    }

    public override void Draw()
    {
        Animator weakEnemyAnimator = this.GetComponent<Animator>();
        weakEnemyAnimator.Play("WeakEnemy_Draw");
        base.Draw();
    }

    protected override void Shoot()
    {
        Animator weakEnemyAnimator = this.GetComponent<Animator>();
        weakEnemyAnimator.Play("WeakEnemy_Shoot");
        base.Shoot();
    }

}


