using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : Enemy
{
    public override void InstantiateEnemy(float new_drawWindow, float new_drawTime, GameManager new_gameManager)
    {
        shootTime = 3.0f;
        base.InstantiateEnemy(new_drawWindow, new_drawTime, new_gameManager);
    }
}
