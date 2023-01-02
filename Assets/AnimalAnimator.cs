using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    [SerializeField] protected AudioClip[] animalSounds;

    public void Start()
    {
        StartCoroutine(PlayAnimalSoundsCoroutine(4.0f, 5.0f));
        StartCoroutine(MoveAnimalCoroutine());
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
    protected IEnumerator MoveAnimalCoroutine()
    {
        while (true)
        {
            Vector2 newPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0.0f, 4.0f));
            //if new position is left of current position, flip the sprite
            if (newPosition.x < this.transform.position.x)
            {
                this.transform.localScale = new Vector3(-2, 2);
            } else
            {
                this.transform.localScale = new Vector3(2, 2);
            }
            float speed = 1.0f;
            while (Vector3.Distance(this.transform.position, newPosition) > 0.1f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, newPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
            
    //Plays a random sound from the animalSounds array at a random moment between x and y seconds
    protected virtual IEnumerator PlayAnimalSoundsCoroutine(float minimal_time, float maximum_time)
    {
        while (true)
        {
            float time = Random.Range(minimal_time, maximum_time);
            yield return new WaitForSeconds(time);
            AudioClip sound = animalSounds[Random.Range(0, animalSounds.Length)];
            CreateAudioSource(sound, sound.name);
        }
    }
}