using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyPlayerAnimator : PlayerAnimator
{
    private Coroutine dyingCoroutine;
 
    
    public void Awake()
    {
        character_name = "cowboy";
    }
    //function that plays the Cowboy_die animation then plays the die sound and then checks if the die sound has finished and plays the Cowboy_die_2 animation when the die sound has finished
    public override void Die()
    {
        playerAnimator.Play("Cowboy_die");
        AudioSource dieSoundSource = playSound(dieSound);
        dyingCoroutine = StartCoroutine(DieSoundFinished(dieSoundSource));
    }

    IEnumerator DieSoundFinished(AudioSource dieSoundSource)
    {
        yield return new WaitUntil(() => !dieSoundSource);
        playerAnimator.Play("Cowboy_die_2");
        playSound(fallSound);
    }

    public override void EarlyShot()
    {
        playerAnimator.Play("Cowboy_earlyshot");
    }

    public override void Draw()
    {
        playerAnimator.Play("Cowboy_draw");
        playSound(drawSound);
    }

    public override void Shoot()
    {
        playerAnimator.Play("Cowboy_shoot", -1, 0);
        playSound(shotSound);
    }

    public override void ResetPlayer()
    {
        if (dyingCoroutine != null)
        {
            StopCoroutine(dyingCoroutine);
        }
        playerAnimator.Play("Cowboy_idle", -1, 0);
    }
}
