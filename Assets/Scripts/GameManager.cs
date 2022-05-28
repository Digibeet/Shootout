using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject weakEnemy;
    [SerializeField] private GameObject fastEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Text levelText;
 
    private int level;
    private List<Enemy> enemies = new List<Enemy>();
    private bool drawn;

    void Start()
    {
        level = DifficultyManager.Instance.GetLevel();
        IntroduceLevel();
        PlanEnemies();
        drawn = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!drawn)
            {
                EarlyShot();
            }
        }
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
            CreateEnemy(weakEnemy, "WeakEnemy", AssignRandomPosition(availablePositions));
        }
        for (int fastEnemyCounter = 0; fastEnemyCounter < numberOfFastEnemies; fastEnemyCounter++)
        {
            Debug.Log("spawning fast enemy " + fastEnemyCounter + 1);
            CreateEnemy(fastEnemy, "FastEnemy", AssignRandomPosition(availablePositions));
        }

        float drawTime = 3.0f - level / 5;
        DuelManager dueler = new DuelManager(drawTime, 1.0f, enemies);
        dueler.StartDuel();
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

    private Vector2 AssignRandomPosition(List<Vector2> availablePositions)
    {
        int indexOfPosition = Random.Range(0, availablePositions.Count);
        Vector2 selectedPosition = availablePositions[indexOfPosition];
        availablePositions.RemoveAt(indexOfPosition);
        return selectedPosition;
    }

    private void CreateEnemy(GameObject enemy, string enemyScript, Vector2 enemyPosition)
    {
        GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
        Enemy newEnemyScript = newEnemy.GetComponent(enemyScript) as Enemy;
        newEnemyScript.InstantiateEnemy(this);
        enemies.Add(newEnemyScript);
    }

    private void Lose()
    {
        Destroy(player);
        StartCoroutine(EndGame());
    }

    public void EarlyShot()
    {
        Lose();
    }

    public void TooLate()
    {
        Lose();
    }

    private IEnumerator EndGame()
    {
        float sadCount = 0f;
        while (sadCount <= 4)
        {
            sadCount += Time.deltaTime;
            //Debug.Log("Counting down draw " + drawCount);
            yield return null;
        }
        SceneManager.LoadScene("TitleScene");
    }
}
