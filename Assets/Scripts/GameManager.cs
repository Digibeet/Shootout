using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject weakEnemy;
    [SerializeField] private GameObject fastEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Text levelText;
    private float drawTime;
    private float randomTimeNoise = 1.0f;
    private int level;
    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        level = DifficultyManager.Instance.getLevel();
        drawTime = 3.0f - level / 5;
        IntroduceLevel();
        PlanEnemies();
    }

    private void IntroduceLevel()
    {
        string new_levelText = "ROUND " + level;
        levelText.text = new_levelText;
    }

    private void PlanEnemies()
    {
        int numberOfWeakEnemies = level;
        int numberOfFastEnemies = level / 2;
        int totalEnemies = numberOfFastEnemies + numberOfWeakEnemies;
        List<Vector2> availablePositions = CreatePositions(totalEnemies);
        for (int weakEnemyCounter = 0; weakEnemyCounter < numberOfWeakEnemies; weakEnemyCounter++)
        {
            Debug.Log("spawning weak enemy " + weakEnemyCounter + 1);
            CreateEnemy(weakEnemy, "WeakEnemy", assignRandomPosition(availablePositions));
        }
        for (int fastEnemyCounter = 0; fastEnemyCounter < numberOfFastEnemies; fastEnemyCounter++)
        {
            Debug.Log("spawning fast enemy " + fastEnemyCounter + 1);
            CreateEnemy(fastEnemy, "FastEnemy", assignRandomPosition(availablePositions));
        }
        StartCoroutine(CountdownToDraw());
    }

    private IEnumerator CountdownToDraw()
    {
        Enemy drawingEnemy = getRandomEnemy();
        float drawCount = 0f;
        float randomizedDrawTime = drawTime + Random.Range(0,randomTimeNoise);
        while (drawCount <= randomizedDrawTime)
        {
            drawCount += Time.deltaTime;
            //Debug.Log("Counting down draw " + drawCount);
            yield return null;
        }
        drawingEnemy.Draw();
        Debug.Log("Enemies left " + enemies.Count);
        if (enemies.Count > 0)
        {
            Debug.Log("Drawing new enemy");
            StartCoroutine(CountdownToDraw());
        }
    }
    private List<Vector2> CreatePositions(int numberOfPositions)
    {
        List<Vector2> openPositions = new List<Vector2>();
        for(int positionIndex = 0; positionIndex < numberOfPositions; positionIndex++)
        {
            Vector2 newPosition = new Vector2(3 * positionIndex, -3);
            openPositions.Add(newPosition);
        }
        return openPositions;
    }

    private Vector2 assignRandomPosition(List<Vector2> availablePositions)
    {
        int indexOfPosition = Random.Range(0, availablePositions.Count);
        Vector2 selectedPosition = availablePositions[indexOfPosition];
        availablePositions.RemoveAt(indexOfPosition);
        return selectedPosition;
    }

    private Enemy getRandomEnemy()
    {
        int indexOfPosition = Random.Range(0, enemies.Count);
        Enemy selectedEnemy = enemies[indexOfPosition];
        enemies.RemoveAt(indexOfPosition);
        return selectedEnemy;
    }

    private void CreateEnemy(GameObject enemy, string enemyScript, Vector2 enemyPosition)
    {
        GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
        Enemy newEnemyScript = newEnemy.GetComponent(enemyScript) as Enemy;
        newEnemyScript.InstantiateEnemy(this);
        enemies.Add(newEnemyScript);
    }

    public void EarlyShot()
    {
        Destroy(player);
    }

    public void TooLate()
    {
        Destroy(player);
    }
}
