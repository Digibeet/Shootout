using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [SerializeField] protected AudioClip music;
    [SerializeField] protected AudioClip duellSound;
    public virtual void PlayAmbiantSounds() {
        AudioSource musicAudioSource = CreateAudioSource(music, "MusicPlayer");
        musicAudioSource.Play();
        musicAudioSource.loop = true;
    }

    public abstract void PlayAnimations();

    public abstract void PlayDuellStart();

    public virtual AudioSource CreateAudioSource(AudioClip audio, string name) {
        GameObject audioSourceObject = new GameObject(name);
        audioSourceObject.transform.SetParent(this.transform);
        AudioSource newAudioSource = audioSourceObject.AddComponent<AudioSource>();
        newAudioSource.clip = audio;
        return newAudioSource;
    }
}
