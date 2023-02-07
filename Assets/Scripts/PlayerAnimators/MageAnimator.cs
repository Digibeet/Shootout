using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAnimator : PlayerAnimator
{
    public void Awake()
    {
        character_name = "Mage";
    }
    //function that plays the Cowboy_die animation then plays the die sound and then checks if the die sound has finished and plays the Cowboy_die_2 animation when the die sound has finished
    public override void Die()
    {
        playerAnimator.Play("Mage_die");
        AudioSource dieSoundSource = PlaySound(dieSound);
    }

    public override void Draw()
    {
        playerAnimator.Play("Mage_draw");
        PlaySound(drawSound);
    }

    public override void Shoot()
    {
        playerAnimator.Play("Mage_shoot", -1, 0);
        PlaySound(shotSound);
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Mage_idle", -1, 0);
    }
}
