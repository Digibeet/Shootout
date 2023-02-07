using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameStart;
    [SerializeField] private GameObject FadeOut;

    public void StartNewGame()
    {
        DifficultyManager.Restart();
        StartCoroutine(StartGame());
    }

    public void StartVersus()
    {
        gameStart.Play();
        SceneManager.LoadScene("Versus");
    }

    IEnumerator StartGame()
    {
        Debug.Log("Starting new game");
        gameStart.Play();
        FadeOut.SetActive(true);
        while (gameStart.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("Level_1");
    }

}