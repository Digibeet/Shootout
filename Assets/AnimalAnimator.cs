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
        float MaximumXCoordinate = 10.0f;
        float MinimumXCoordinate = -10.0f;
        float MinimumYCoordinate = 0.0f;
        float MaximumYCoordinate = 5.0f;
        
        while (true)
        {
            Vector3 direction = new Vector3(Random.Range(MinimumXCoordinate, MaximumXCoordinate), Random.Range(-MinimumYCoordinate, MaximumYCoordinate), 0);
            //if direction in x is less than 0 flip the animal
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            }
            float speed = 0.3f;
            float duration = Random.Range(1f, 3f);
            float time = 0;
            while (time < duration && transform.position.x < MaximumXCoordinate && transform.position.x > MinimumXCoordinate && transform.position.y < MaximumYCoordinate && transform.position.y > MinimumYCoordinate)
            {
                transform.position = Vector3.Lerp(transform.position, direction, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            {
                transform.position += direction * speed * Time.deltaTime;
                time += Time.deltaTime;
                yield return null;
            }
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