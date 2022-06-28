using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameObject Gunshot;
    [SerializeField] private GameObject Lightning;
    [SerializeField] private Text Feedback;

    public GameObject weakEnemy;
    public GameObject fastEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Text levelText;
    [SerializeField] private bool drawStarted;
    private EnemyManager enemyManager;

    public int level { get; private set; }
    private List<Enemy> enemies = new List<Enemy>();

    private Animator playerAnimator;

    [SerializeField] GameObject bulletUI;
    int bullets_left = 6;
    bool lost = false;

    void Start()
    {
        drawStarted = false;
        bulletUI.GetComponent<BulletManager>().SetBulletImage(bullets_left);
        playerAnimator = player.GetComponent<Animator>();
        level = DifficultyManager.Instance.GetLevel();
        IntroduceLevel();
        enemyManager = new EnemyManager(level, this);
        enemyManager.PlanEnemies();
        Debug.Log("Enemies planned");
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if (!lost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                if (!drawStarted)
                {
                    EarlyShot();
                }
            }
        }
    }

    private void Shoot()
    {
        playerAnimator.Play("Shoot_Player", -1, 0);
        Instantiate(Gunshot, new Vector2(0,0), Quaternion.identity);
        bullets_left--;
        bulletUI.GetComponent<BulletManager>().SetBulletImage(bullets_left);
    }

    private void IntroduceLevel()
    {
        string new_levelText = "ROUND " + level;
        levelText.text = new_levelText;
    }
    public void CreateEnemy(GameObject enemy, Vector2 enemyPosition)
    {
        GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
        Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();
        newEnemyScript.InstantiateEnemy(this);
        enemies.Add(newEnemyScript);
    }

    private IEnumerator StartGame()
    {
        Debug.Log("Starting game");
        soundManager.PlayWind();
        soundManager.PlayMusic();
        float startCount = 0.0f;
        float startTime = 7.0f + Random.Range(0, 1);
        while (startCount <= startTime)
        {
            startCount += Time.deltaTime;
            //Debug.Log("Counting down draw " + startCount);
            yield return null;
        }
        drawStarted = true;
        StartDuel();
        Debug.Log(drawStarted);
    }

    private void StartDuel()
    {
        Debug.Log("Starting duel");
        soundManager.PlayCrows();
        Enemy drawingEnemy = enemyManager.GetRandomEnemy(enemies);
        DifficultyManager.Instance.UnlockCursor();
        drawingEnemy.Draw();
        playerAnimator.Play("Draw_Player");
        //soundManager.PlayChurch();
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
            StartCoroutine(Victory());
        }
        else
        {
            StartDuel();
        }
    }

    private IEnumerator Victory()
    {
        Debug.Log("Level won");
        DifficultyManager.Instance.LockCursor();
        float startCount = 0.0f;
        float startTime = 3.0f;
        while (startCount <= startTime)
        {
            startCount += Time.deltaTime;
            yield return null;
        }
        DifficultyManager.Instance.NextLevel();
        if(DifficultyManager.Instance.GetLevel() == 5)
        {
            DifficultyManager.Instance.NextAct();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }

    private void Lose()
    {
        //Destroy(player);
        lost = true;
        StartCoroutine(EndGame());
    }

    public void EarlyShot()
    {
        Lightning.GetComponent<SpriteRenderer>().enabled = true;
        Lightning.GetComponent<Animator>().Play("Flash", -1, 0);
        playerAnimator.Play("burn");
        Feedback.enabled = true;
        Lose();
    }

    public void TooLate()
    {
        playerAnimator.Play("Player_Die");
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
