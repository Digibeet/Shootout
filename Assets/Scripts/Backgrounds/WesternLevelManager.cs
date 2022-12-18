using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesternLevelManager : LevelManager
{
    public override void PlayAnimations()
    {
        throw new System.NotImplementedException();
    }

    public override void PlayDuellStart()
    {
        AudioSource duellStartAudioSource = CreateAudioSource(duellSound, "DuellSoundPlayer");
        duellStartAudioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
