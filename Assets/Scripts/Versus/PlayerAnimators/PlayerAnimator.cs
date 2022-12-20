using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimator : MonoBehaviour
{
    protected Animator playerAnimator;
    public Sprite characterTumbnail;
    public string character_name;

    public virtual void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public abstract void Draw();
    public abstract void Shoot();
    public abstract void Die();
    public abstract void EarlyShot();
}
