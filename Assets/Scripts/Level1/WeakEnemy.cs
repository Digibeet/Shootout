using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : Enemy
{
    // POLYMORPHISM
    public override void InstantiateEnemy(GameManager new_gameManager)
    {
        shootTime = 3.0f/(new_gameManager.level%5);
        base.InstantiateEnemy(new_gameManager);
    }
}


