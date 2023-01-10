using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int player1_score = 4;
    private static int player2_score = 0;
    
    public static ScoreManager Instance; 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ResetScore()
    {
        player1_score = 0;
        player2_score = 0;
    }

    public static void IncreaseScore(int player)
    {
        if (player == 1)
        {
            player1_score++;
        }
        else if (player == 2)
        {
            player2_score++;
        }
    }

    public static int GetScore(int player)
    {
        if (player == 1)
        {
            return player1_score;
        }
        else if (player == 2)
        {
            return player2_score;
        }
        else
        {
            return 0;
        }
    }

}
