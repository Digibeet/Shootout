using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    // ENCAPSULATION
    [SerializeField] private static int level { get; set; } = 1;
    [SerializeField] protected Texture2D cursorTexture;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(32, 32), CursorMode.Auto);
    }

    public static void Restart()
    {
        level = 1;
    }

    public static int GetLevel()
    {
        return level;
    }

    public static void NextLevel()
    {
        level++;
    }
}
