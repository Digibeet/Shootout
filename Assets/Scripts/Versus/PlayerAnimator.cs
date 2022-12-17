using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnimator;

    public void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    public abstract void Draw();
    public abstract void Shoot();
    public abstract void Die();
    public abstract void EarlyShot();
}
