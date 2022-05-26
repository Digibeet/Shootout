using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    public override void InstantiateEnemy(float new_drawWindow, float new_drawTime, GameManager new_gameManager)
    {
        shootTime = 1.0f;
        base.InstantiateEnemy(new_drawWindow, new_drawTime, new_gameManager);
    }
}
