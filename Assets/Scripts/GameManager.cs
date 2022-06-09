using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject weakEnemy;
    [SerializeField] public GameObject fastEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Text levelText;
    [SerializeField] private bool drawStarted;
    private EnemyManager enemyManager;

    private int level;
    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        drawStarted = false;
        level = DifficultyManager.Instance.GetLevel();
        IntroduceLevel();
        enemyManager = new EnemyManager(level, this);
        enemyManager.PlanEnemies();
        Debug.Log("Enemies planned");
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!drawStarted)
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
    public void CreateEnemy(GameObject enemy, string enemyScript, Vector2 enemyPosition)
    {
        GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
        Enemy newEnemyScript = newEnemy.GetComponent(enemyScript) as Enemy;
        newEnemyScript.InstantiateEnemy(this);
        enemies.Add(newEnemyScript);
    }

    private IEnumerator StartGame()
    {
        Debug.Log("Starting game");
        float startCount = 0.0f;
        float startTime = 3.0f + Random.Range(0, 1);
        while (startCount <= startTime)
        {
            startCount += Time.deltaTime;
            //Debug.Log("Counting down draw " + startCount);
            yield return null;
        }
        float drawTime = 3.0f - level / 5;
        drawStarted = true;
        StartDuel();
        Debug.Log(drawStarted);
    }

    private void StartDuel()
    {
        Debug.Log("Starting duel");
        Enemy drawingEnemy = enemyManager.GetRandomEnemy(enemies);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.Play("Player_Draw");
        drawingEnemy.Draw();
        Debug.Log("Enemies left " + enemies.Count);
        if (enemies.Count > 0)
        {
            Debug.Log("Drawing new enemy");
        }
    }

    public void CheckVictory()
    {
        Debug.Log("Checking for victory");
        if(enemies.Count <= 0)
        {
            Victory();
        }
        else
        {
            StartDuel();
        }
    }

    private void Victory()
    {
        Debug.Log("Level won");
        DifficultyManager.Instance.NextLevel();
        SceneManager.LoadScene("MainGameScene");
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
