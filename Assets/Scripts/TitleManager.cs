using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameStart;
    public void StartNewGame()
    {
        DifficultyManager.Instance.Restart();
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        Debug.Log("Starting new game");
        gameStart.Play();
        DifficultyManager.Instance.LockCursor();
        while (gameStart.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("MainGameScene");
    }

}