using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected SoundManager soundManager;
    [SerializeField] private GameObject EmptyRevolver;
    [SerializeField] protected GameObject Lightning;
    [SerializeField] protected Text Feedback;

    public GameObject weakEnemy;
    public GameObject fastEnemy;
    [SerializeField] protected GameObject player;
    [SerializeField] private Text levelText;
    [SerializeField] public static bool drawStarted;
    private EnemyManager enemyManager;

    public int level { get; protected set; }
    private List<Enemy> enemies = new List<Enemy>();

    protected PlayerAnimator playerAnimator;

    [SerializeField] GameObject bulletUI;
    protected string endScene;
    int bullets_left = 6;
    public static bool lost;

    protected virtual void Start()
    {
        lost = false;
        drawStarted = false;
        playerAnimator = player.GetComponent<PlayerAnimator>();
        level = DifficultyManager.Instance.GetLevel();
        endScene = "TitleScene";
        IntroduceLevel();
        enemyManager = new EnemyManager(level, this);
        enemyManager.PlanEnemies();
        Debug.Log("Enemies planned");
        StartCoroutine(StartGame());
    }

    protected virtual void Update()
    {
        if (!lost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (bullets_left > 0)
                {
                    Shoot(playerAnimator);
                    bullets_left = ReduceBullets(bullets_left, 1, bulletUI);
                }
                else
                    NoAmmo(playerAnimator);
                if (!drawStarted)
                {
                    EarlyShot();
                }
            }
        }
    }

    protected int ReduceBullets(int bullets, int reductionAmount, GameObject bulletUI)
    {
        int remainingBullets = bullets - reductionAmount;
        if (remainingBullets < 0)
        {
            return 0;
        }
        else
        {
            int bulletsFired = 6 - remainingBullets;
            bulletUI.GetComponent<Animator>().Play("shoot_" + bulletsFired);
            return remainingBullets;
        }
    }

    protected void Shoot(PlayerAnimator shootingPlayer)
    {
        shootingPlayer.Shoot();
    }

    protected void NoAmmo(PlayerAnimator shootingPlayer)
    {
        shootingPlayer.EmptyClip();
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

    protected void CreateLightning(GameObject lightning, Vector2 lightningPosition)
    {
        GameObject newLightning = Instantiate(lightning, lightningPosition, Quaternion.identity);
        newLightning.GetComponent<SpriteRenderer>().enabled = true;
        newLightning.GetComponent<Animator>().Play("Flash", -1, 0);
    }

    public IEnumerator StartGame()
    {
        Debug.Log("Starting game");
        soundManager.PlayWind();
        soundManager.PlayMusic();
        float startCount = 0.0f;
        float startTime = 5.0f + Random.Range(0, 1);
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

    public virtual void StartDuel()
    {
        Debug.Log("Starting duel");
        soundManager.PlayCrows();
        Enemy drawingEnemy = enemyManager.GetRandomEnemy(enemies);
        DifficultyManager.Instance.UnlockCursor();
        drawingEnemy.Draw();
        playerAnimator.Draw();
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

    protected void Lose()
    {
        //Destroy(player);
        lost = true;
        StartCoroutine(EndGame());
    }

    public void EarlyShot()
    {
        CreateLightning(Lightning, new Vector2(-3, 0.5f));
        playerAnimator.EarlyShot();
        Feedback.enabled = true;
        Lose();
    }

    public void TooLate()
    {
        playerAnimator.Die();
        Lose();
    }

    protected virtual IEnumerator EndGame()
    {
        float sadCount = 0f;
        while (sadCount <= 4)
        {
            sadCount += Time.deltaTime;
            //Debug.Log("Counting down draw " + drawCount);
            yield return null;
        }
        SceneManager.LoadScene(endScene);
    }

    public void ChangeLevelText(string newLevelText)
    {
        levelText.text = newLevelText;
    }
}
