using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 1;
    protected void OnMouseDown()
    {
        Debug.Log("Clicking on player");
        if (GameManager.drawStarted && !GameManager.lost)
        {
            Hit();
        }
    }
    protected void KillEnemy()
    {
        Debug.Log("Enemy killed");
        //GameManager.lost = true;
        this.GetComponent<Animator>().Play("Player_Die");
        //Destroy(gameObject);
    }

    protected virtual void Hit()
    {
        Debug.Log("Enemy hit");
        Debug.Log(lives + "Lives left");
        lives--;
        if (lives <= 0)
        {
            KillEnemy();
        }
    }
}
