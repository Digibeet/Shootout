using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [SerializeField] protected AudioClip music;
    [SerializeField] protected AudioClip backgroundSounds;
    [SerializeField] protected AudioClip duellSound;
    protected static GameObject levelObjectsParent;

    protected void Awake()
    {
        Debug.Log("Starting the levelManager");
        levelObjectsParent = GameObject.Find("LevelObjects");
        if (levelObjectsParent == null)
        {
            levelObjectsParent = new GameObject("LevelObjects");
        }
    }

    public void InitiateLevel()
    {
        DestroyLevelObjects();
        SpawnObjects();
        PlayAmbiantSounds();
    }

    public virtual void PlayAmbiantSounds() {
        if (music)
        {
            AudioSource musicAudioSource = CreateAudioSource(music, "MusicPlayer");
            musicAudioSource.Play();
            musicAudioSource.loop = true;
        }
    }

    protected void DestroyLevelObjects()
    {
        foreach (Transform child in levelObjectsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public abstract void SpawnObjects();

    public abstract void PlayDuellStart();

    public virtual AudioSource CreateAudioSource(AudioClip audio, string name) {
        GameObject audioSourceObject = new GameObject(name);
        audioSourceObject.transform.SetParent(this.transform);
        AudioSource newAudioSource = audioSourceObject.AddComponent<AudioSource>();
        newAudioSource.clip = audio;
        return newAudioSource;
    }
}
