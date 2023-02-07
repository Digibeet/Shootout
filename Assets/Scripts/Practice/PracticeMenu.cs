using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PracticeMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject GameManager;
    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartButton()
    {
        Menu.SetActive(false);
        gameplayUI.SetActive(true);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void Lost()
    {
        Menu.SetActive(true);
        gameplayUI.SetActive(false);
    }
}
