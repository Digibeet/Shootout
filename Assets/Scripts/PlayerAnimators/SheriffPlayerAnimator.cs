using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffPlayerAnimator : PlayerAnimator
{
    [SerializeField] private AudioClip gunsDropping;
    public override void Die()
    {
        playerAnimator.Play("Sheriff_die");
        PlaySound(dieSound);
    }

    private void DieContinue()
    {
        playerAnimator.Play("Sheriff_die_2");
        PlaySound(fallSound);
    }
    
    private void GunsDropping()
    {
        PlaySound(gunsDropping);
    }

    public override void Draw()
    {
        playerAnimator.Play("Sheriff_draw");
        PlaySound(drawSound);
    }

    public override void EarlyShot()
    {
        
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Sheriff_idle");
    }

    public override void Shoot()
    {
        playerAnimator.Play("Sheriff_shoot", -1, 0);
        PlaySound(shotSound);
    }
}
