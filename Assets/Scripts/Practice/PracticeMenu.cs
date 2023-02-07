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
    private GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameManager.GetComponent<GameManager>();
    }
    
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
        gameManagerScript.StartGame();
    }

    public void EndGame()
    {
        Menu.SetActive(true);
        gameplayUI.SetActive(false);
    }
}
