using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainClawAnimator : PlayerAnimator
{
    [SerializeField] AudioClip drawLine;
    public override void ResetPlayer()
    {
        playerAnimator.Play("CaptainClaw_idle");
    }

    public override void Draw()
    {
        playerAnimator.Play("CaptainClaw_draw");
        PlaySound(drawSound);
        PlaySound(drawLine);
    }

    public override void Shoot()
    {
        playerAnimator.Play("CaptainClaw_shoot", -1, 0);
        PlaySound(shotSound);
    }

    public override void Die()
    {
        playerAnimator.Play("CaptainClaw_die");
        PlaySound(dieSound);
    }

    public override (float, float) GetBulletMeasurements()
    {
        return (20, 20);
    }
}