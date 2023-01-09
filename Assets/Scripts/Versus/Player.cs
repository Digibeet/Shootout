using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 1;
    
    public virtual bool Hit()
    {
        Debug.Log("Enemy hit");
        Debug.Log(lives + "Lives left");
        lives--;
        if (lives <= 0)
        {
            this.GetComponent<PlayerAnimator>().Die();
            return true;
        }
        return false;
    }
}
