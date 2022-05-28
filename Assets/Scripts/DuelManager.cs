using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ABSTRACTION
public class DuelManager
{
    private float drawTime;
    private float randomTimeNoise;
    private List<Enemy> enemies;
    public DuelManager(float new_drawTime, float new_randomTimeNoise, List<Enemy> new_enemies)
    {
        drawTime = new_drawTime;
        randomTimeNoise = new_randomTimeNoise;
        enemies = new_enemies;
    }
    public void StartDuel()
    {
        Debug.Log("Starting duel");
        Enemy drawingEnemy = getRandomEnemy();
        drawingEnemy.StartDraw(randomTimeNoise);
        Debug.Log("Enemies left " + enemies.Count);
        if (enemies.Count > 0)
        {
            Debug.Log("Drawing new enemy");
        }
    }
    
    private Enemy getRandomEnemy()
    {
        int indexOfPosition = Random.Range(0, enemies.Count);
        Enemy selectedEnemy = enemies[indexOfPosition];
        enemies.RemoveAt(indexOfPosition);
        return selectedEnemy;
    }
}
