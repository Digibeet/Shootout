using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 1;

    protected void KillPlayer()
    {
        GameManager.lost = true;
        this.GetComponent<PlayerAnimator>().Die();
        //Destroy(gameObject);
    }

    public virtual void Hit()
    {
        Debug.Log("Enemy hit");
        Debug.Log(lives + "Lives left");
        lives--;
        if (lives <= 0)
        {
            KillPlayer();
        }
    }
}
