using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    [SerializeField] private static int level { get; set; } = 1;
    [SerializeField] private static int score { get; set; } = 0;

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

    void Start()
    {
        
    }

    public int getLevel()
    {
        return level;
    }

    public void NextLevel()
    {
        level = level +1;
    }

    public void increaseScore(int scoreIncrease)
    {
        score = score = scoreIncrease;
    }
}
