using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartNewGame()
    {
        Debug.Log("Starting new game");
        DifficultyManager.Instance.Restart();
        SceneManager.LoadScene("MainGameScene");
    }

}