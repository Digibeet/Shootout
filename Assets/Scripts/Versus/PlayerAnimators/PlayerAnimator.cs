using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimator : MonoBehaviour
{
    protected Animator playerAnimator;
    public Sprite characterTumbnail;
    public string character_name;
    [SerializeField] protected AudioClip shotSound;
    [SerializeField] protected AudioClip drawSound;
    [SerializeField] protected AudioClip dieSound;
    [SerializeField] protected AudioClip emptyClipSound;

    public virtual void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    //function to instantiate an empty GameObject with the sounds object as parent to play a given sound
    protected AudioSource playSound(AudioClip sound)
    {
        GameObject soundObject = new GameObject();
        soundObject.transform.parent = GameObject.Find("Sounds").transform;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
        Destroy(soundObject, sound.length);
        return audioSource;
    }


    public abstract void Draw();
    public abstract void Shoot();
    public abstract void Die();
    public abstract void EarlyShot();
    public virtual void EmptyClip()
    {
        playSound(emptyClipSound);
    }
}
