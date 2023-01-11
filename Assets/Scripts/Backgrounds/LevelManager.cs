using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] protected AudioClip music;
    [SerializeField] protected List<AudioClip> backgroundSounds;
    [SerializeField] protected AudioClip duellSound;
    protected static GameObject levelObjectsParent;

    protected void Awake()
    {
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


    protected void DestroyLevelObjects()
    {
        foreach (Transform child in levelObjectsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    protected virtual void PlayAmbiantSounds()
    {
        StartCoroutine(PlayBackgroundSounds());
    }

    public virtual void SpawnObjects()
    {
        
    }

    private IEnumerator PlayBackgroundSounds()
    {
        if (backgroundSounds.Count > 0)
        {
            while (true)
            {
                int randomSound = Random.Range(0, backgroundSounds.Count - 1);
                Debug.Log(randomSound);
                AudioSource audioSource = ScoreManager.playSound(backgroundSounds[randomSound]);
                Debug.Log("playing random background sound");
                yield return new WaitUntil(() => !audioSource);
            }
        }
    }

    protected AudioSource CreateAudioSource(AudioClip clip, string name)
    {
        GameObject audioSourceObject = new GameObject(name);
        audioSourceObject.transform.parent = levelObjectsParent.transform;
        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        return audioSource;
    }
}
