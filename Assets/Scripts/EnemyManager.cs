using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{ 
    private int level;
    private GameManager gameManager;

    public EnemyManager(int new_level, GameManager new_gameManager)
    {
        level = new_level;
        gameManager = new_gameManager;
    }

    public void PlanEnemies()
    {
        int numberOfWeakEnemies = level;
        //Debug.Log("Spawning " + numberOfWeakEnemies + " weak enemies");
        int numberOfFastEnemies = level / 2;
        int totalEnemies = numberOfFastEnemies + numberOfWeakEnemies;
        List<Vector2> availablePositions = CreatePositions(totalEnemies);
        for (int weakEnemyCounter = 0; weakEnemyCounter < numberOfWeakEnemies; weakEnemyCounter++)
        {
            Debug.Log("spawning weak enemy " + weakEnemyCounter);
            gameManager.CreateEnemy(gameManager.weakEnemy, "WeakEnemy", AssignRandomPosition(availablePositions));
        }
        for (int fastEnemyCounter = 0; fastEnemyCounter < numberOfFastEnemies; fastEnemyCounter++)
        {
            Debug.Log("spawning fast enemy " + fastEnemyCounter + 1);
            gameManager.CreateEnemy(gameManager.fastEnemy, "FastEnemy", AssignRandomPosition(availablePositions));
        }
        Debug.Log("Spawned all enemies");
        return;
    }

    private List<Vector2> CreatePositions(int numberOfPositions)
    {
        List<Vector2> openPositions = new List<Vector2>();
        for (int positionIndex = 0; positionIndex < numberOfPositions; positionIndex++)
        {
            Vector2 newPosition = new Vector2(2 * positionIndex+1, -2);
            openPositions.Add(newPosition);
        }
        return openPositions;
    }

    private Vector2 AssignRandomPosition(List<Vector2> availablePositions)
    {
        int indexOfPosition = Random.Range(0, availablePositions.Count);
        Vector2 selectedPosition = availablePositions[indexOfPosition];
        availablePositions.RemoveAt(indexOfPosition);
        return selectedPosition;
    }

    public Enemy GetRandomEnemy(List<Enemy> enemies)
    {
        int indexOfPosition = Random.Range(0, enemies.Count);
        Enemy selectedEnemy = enemies[indexOfPosition];
        enemies.RemoveAt(indexOfPosition);
        return selectedEnemy;
    }
}
