using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalAnimator : MonoBehaviour
{
    [SerializeField] protected AudioClip[] animalSounds;
    protected Animator animalAnimator;

    public void Awake()
    {
        animalAnimator = GetComponent<Animator>();
    }

    public void Start()
    {
        MoveAnimal();
        PlayAnimalSounds();
    }

    //Creates an audio source with the given audio clip and name and destroys the audio source after the clip is finished playing
    protected AudioSource CreateAudioSource(AudioClip audio, string name)
    {
        GameObject audioSourceObject = new GameObject(name);
        audioSourceObject.transform.SetParent(this.transform);
        AudioSource newAudioSource = audioSourceObject.AddComponent<AudioSource>();
        newAudioSource.clip = audio;
        Destroy(audioSourceObject, audio.length);
        return newAudioSource;
    }

    //Coroutine that moves the object to a random position within set bounds at a steady speed
    protected abstract void MoveAnimal();
    
    //Plays a random sound from the animalSounds array at a random moment between x and y seconds
    protected abstract void PlayAnimalSounds();
}