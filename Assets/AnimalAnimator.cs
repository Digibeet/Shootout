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

    protected virtual IEnumerator MoveAnimalCoroutine()
    {
        while (true)
        {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            //if direction in x is less than 0 flip the animal
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            }
            float speed = Random.Range(0.5f, 1.5f);
            float duration = Random.Range(1f, 3f);
            float time = 0;
            while (time < duration)
            {
                transform.position += direction * speed * Time.deltaTime;
                time += Time.deltaTime;
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