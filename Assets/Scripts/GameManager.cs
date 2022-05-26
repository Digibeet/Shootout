using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject weakEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Text levelText;
    private int level;
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        level = DifficultyManager.Instance.getLevel();
        IntroduceLevel();
        PlanEnemies();
    }

    private void IntroduceLevel()
    {
        
        string new_levelText = "ROUND " + level;
        levelText.text = new_levelText;
    }

    void PlanEnemies()
    {
        int numberOfWeakEnemies = level;
        int numberOfFastEnemies = level / 2;
        for (int enemyCounter = 0; enemyCounter < numberOfWeakEnemies; enemyCounter++)
        {
            Debug.Log("spawning weak enemy " + enemyCounter);
            CreateEnemy(weakEnemy, "WeakEnemy");
        }
    }

    private void CreateEnemy(GameObject enemy, string enemyScript)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector2(5, -3), Quaternion.identity);
        Enemy newEnemyScript = newEnemy.GetComponent(enemyScript) as Enemy;
        newEnemyScript.InstantiateEnemy(2.0f, 1.0f, this);
        enemies.Add(newEnemy);
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
