using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject crows;
    [SerializeField] private GameObject church;
    [SerializeField] private GameObject wind;
    public void PlayCrows()
    {
        AudioSource crowsSource = crows.GetComponent<AudioSource>();
        float clipLength = crowsSource.clip.length;
        crowsSource.time = Random.Range(0, clipLength);
        crowsSource.Play();
    }

    public void PlayChurch()
    {
        church.GetComponent<AudioSource>().Play();
    }

    public void PlayWind()
    {
        wind.GetComponent<AudioSource>().Play();
    }
}
