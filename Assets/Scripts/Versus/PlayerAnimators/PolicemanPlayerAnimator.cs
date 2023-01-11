using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanPlayerAnimator : PlayerAnimator
{
    [SerializeField] protected AudioClip fallSound;
    public void Awake()
    {
        character_name = "policeman";
    }

    public override void Die()
    {
        playerAnimator.Play("Policeman_die");
        AudioSource dieSoundSource = playSound(dieSound);
        StartCoroutine(DieSoundFinished(dieSoundSource));
    }

    IEnumerator DieSoundFinished(AudioSource dieSoundSource)
    {
        yield return new WaitUntil(() => !dieSoundSource);
        playerAnimator.Play("Policeman_die_2");
        playSound(fallSound);
    }

    public override void EarlyShot()
    {

    }

    public override void Draw()
    {
        playerAnimator.Play("Policeman_draw");
        playSound(drawSound);
    }

    public override void Shoot()
    {
        playerAnimator.Play("Policeman_shoot", -1, 0);
        playSound(shotSound);
    }

    public override void ResetPlayer()
    {
        playerAnimator.Play("Policeman_idle");
    }
}
