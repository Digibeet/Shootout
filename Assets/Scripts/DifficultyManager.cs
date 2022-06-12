using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    // ENCAPSULATION
    [SerializeField] private static int level { get; set; } = 1;
    [SerializeField] private static int score { get; set; } = 0;
    [SerializeField] private Texture2D crosshair;

    void Awake()
    {
        Cursor.SetCursor(crosshair, new Vector2(31,31), CursorMode.ForceSoftware);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }    
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        level = 1;
        score = 0;
    }

    public int GetLevel()
    {
        return level;
    }

    public void NextLevel()
    {
        level++;
    }

    public void increaseScore(int scoreIncrease)
    {
        score = score = scoreIncrease;
    }
}
