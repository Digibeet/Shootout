using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesternLevelManager : LevelManager
{

    public override void PlayDuellStart()
    {
        AudioSource duellStartAudioSource = CreateAudioSource(duellSound, "DuellSoundPlayer");
        duellStartAudioSource.Play();
    }

    public override void SpawnObjects()
    {

    }
}
