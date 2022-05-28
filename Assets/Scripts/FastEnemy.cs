using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    // POLYMORPHISM
    public override void InstantiateEnemy(GameManager new_gameManager)
    {
        shootTime = 1.0f;
        base.InstantiateEnemy(new_gameManager);
    }
}
