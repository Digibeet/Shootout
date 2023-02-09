using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public void StartNewGame()
    {
        DifficultyManager.Restart();
        StartGame();
    }

    public void StartVersus()
    {
        SceneManager.LoadScene("Versus");
    }

    void StartGame()
    {
        SceneManager.LoadScene("PracticeScene");
    }
}