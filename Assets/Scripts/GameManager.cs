using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject Lightning;
    [SerializeField] protected GameObject mainCamera;

    [SerializeField] private GameObject UI;
    [SerializeField] protected List<GameObject> tallies;

    [SerializeField] private GameObject enemy;
    private PlayerAnimator enemyAnimator;

    [SerializeField] protected GameObject player;
    protected PlayerAnimator playerAnimator;

    [SerializeField] private Text levelText;
    [SerializeField] public static bool drawStarted;
    
    protected string endScene;
    protected const int maxBullets = 6;
    protected int bulletsLeft_p1 = maxBullets;
    public static bool gameActive;

    protected List<GameObject> trashObjects = new List<GameObject>();

    //Coroutines
    protected Coroutine startGameCoroutineInstance;
    private Coroutine enemyDrawing;

    //UI
    [SerializeField] protected List<GameObject> bulletUI_p1;
    [SerializeField] protected GameObject cheaterUI;
    [SerializeField] protected Text timer;

    //Sounds
    [SerializeField] private List<AudioClip> duellStartClip;
    [SerializeField] protected AudioClip drawStartSound;
    [SerializeField] protected AudioClip earlyShotSound;
    
    //Lights
    [SerializeField] protected GameObject lightObject;
    public static UnityEngine.Rendering.Universal.Light2D globalLight;

    protected virtual void Start()
    {
        gameActive = false;
        drawStarted = false;
        playerAnimator = player.GetComponent<PlayerAnimator>();
        enemyAnimator = enemy.GetComponent<PlayerAnimator>();
        IntroduceLevel();
    }

    protected virtual void Update()
    {
        if (gameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!drawStarted)
                {
                    EarlyShot(player);
                }
                else if (bulletsLeft_p1 > 0)
                {
                    Shoot(playerAnimator);
                    bulletsLeft_p1 = ReduceBullets(bulletsLeft_p1, 1, bulletUI_p1);
                    BoxCollider2D opponentHitBox = enemy.GetComponent<BoxCollider2D>();
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = enemy.transform.position.z;
                    if (opponentHitBox.bounds.Contains(mousePosition))
                    {
                        ScoreManager.IncreaseScore(1);
                        PrintScore(1);
                        enemyAnimator.Die();
                        StopCoroutine(enemyDrawing);
                        DifficultyManager.NextLevel();
                        StartCoroutine(EndGame());
                    }
                }
                else
                    playerAnimator.EmptyClip();             
            }
        }
    }

    public void CleanTrash()
    {
        foreach (GameObject obj in trashObjects)
        {
            Destroy(obj);
        }
    }

    protected int ReduceBullets(int currentBullets, int reductionAmount, List<GameObject> bulletObjects)
    {
        int remainingBullets = currentBullets - reductionAmount;
        if (remainingBullets < 0)
        {
            return 0;
        }
        else
        {
            for (int i = remainingBullets; i < maxBullets; i++)
            {
                bulletObjects[i].SetActive(false);
            }
            return remainingBullets;
        }
    }
    
    protected void ResetBullets(List<GameObject> bulletObjects)
    {
        for(int i = 0; i < maxBullets; i++)
        {
            bulletObjects[i].SetActive(true);
        }
    }

    protected void Shoot(PlayerAnimator shootingPlayer)
    {
        shootingPlayer.Shoot();
    }

    private void IntroduceLevel()
    {
        string new_levelText = "PRACTICE ROUND " + DifficultyManager.GetLevel();
        levelText.text = new_levelText;
    }

    protected void CreateLightning(GameObject lightning, Vector2 lightningPosition)
    {
        GameObject newLightning = Instantiate(lightning, lightningPosition, Quaternion.identity);
        newLightning.GetComponent<SpriteRenderer>().enabled = true;
        newLightning.GetComponent<Animator>().Play("Flash", -1, 0);
        Destroy(newLightning, newLightning.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void InitializeLevel()
    {
        Debug.Log("Loading level " + DifficultyManager.GetLevel());
        CleanTrash();
        drawStarted = false;
        timer.text = "0";
        PrintScore(1);
        bulletsLeft_p1 = maxBullets;
        ResetBullets(bulletUI_p1);
        timer.gameObject.SetActive(false);
        playerAnimator.ResetPlayer();
        enemyAnimator.ResetPlayer();
        IntroduceLevel();
    }

    public void StartGame()
    {
        InitializeLevel();
        gameActive = true;
        int randomClipIndex = Random.Range(0, duellStartClip.Count);
        ScoreManager.PlaySound(duellStartClip[randomClipIndex]);
        startGameCoroutineInstance = StartCoroutine(StartGameCoroutine());
    }

    protected IEnumerator StartGameCoroutine()
    {      
        float startCount = 0.0f;
        float startTime = 2.0f + Random.Range(0, 3);
        while (startCount <= startTime)
        {
            startCount += Time.deltaTime;
            yield return null;
        }
        drawStarted = true;
        StartDuel();
    }

    public virtual void StartDuel()
    {
        ScoreManager.PlaySound(drawStartSound);
        playerAnimator.Draw();
        enemyAnimator.Draw();
        enemyDrawing = StartCoroutine(EnemyShoots());
        StartCoroutine(RunTimer());
    }

    private IEnumerator EnemyShoots()
    {
        float enemyReactionTime = 3 / DifficultyManager.GetLevel();
        float timePassed = 0.0f;
        while(timePassed < enemyReactionTime)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }
        enemyAnimator.Shoot();
        playerAnimator.Die();
        StartCoroutine(EndGame());
    }

    protected void PrintScore(int player)
    {
        int score = ScoreManager.GetScore(player);
        if (score == 0 || player > 2 || player < 1)
        {
            return;
        }
        string tallyParentString;
        float tallyBasePosition;
        float tallyOffset;
        if (player == 1)
        {
            tallyParentString = "Gameplay/Player1/Score_p1";
            tallyBasePosition = -20.0f;
            tallyOffset = 20.0f;
        }
        else
        {
            tallyParentString = "Gameplay/Player2/Score_p2";
            tallyBasePosition = 20.0f;
            tallyOffset = -20.0f;
        }
        Transform scoreParent = UI.transform.Find(tallyParentString);
        List<GameObject> scoreInTallies = ConvertScoreToTallies(score, scoreParent);
        for (int tallyIndex = 0; tallyIndex < scoreInTallies.Count; tallyIndex++)
        {
            GameObject currentTally = scoreInTallies[tallyIndex];
            float xPosition = tallyBasePosition + tallyIndex * tallyOffset;
            Vector2 tallyPosition = new Vector2(xPosition, -20.0f);
            currentTally.transform.localPosition = tallyPosition;
        }
    }

    private List<GameObject> ConvertScoreToTallies(int score, Transform tallyParent)
    {
        foreach (Transform child in tallyParent)
        {
            Destroy(child.gameObject);
        }
        List<GameObject> tallyScore = new List<GameObject>();
        int amountOfFullTallies = Mathf.FloorToInt(score / 5);
        if (amountOfFullTallies > 0)
        {
            for (int i = 0; i < amountOfFullTallies; i++)
            {
                GameObject new_tally = Instantiate(tallies[4], tallyParent);
                tallyScore.Add(new_tally);
            }
        }
        int rest = score % 5;
        if (rest > 0)
        {
            GameObject remainingTally = Instantiate(tallies[rest - 1], tallyParent);
            tallyScore.Add(remainingTally);
        }
        return tallyScore;
    }

    protected virtual void EarlyShot(GameObject earlyPlayer, int playerNumber = 1)
    {
        StartCoroutine(mainCamera.GetComponent<ShakeCamera>().Shake(0.5f));
        foreach (Transform child in GameObject.Find("Sounds").transform)
        {
            Destroy(child.gameObject);
        }
        ScoreManager.PlaySound(earlyShotSound);
        Vector3 cheaterLabelPosition = new Vector3(earlyPlayer.transform.position.x, earlyPlayer.transform.position.y + 1.0f, 0);
        GameObject cheaterLabel = Instantiate(cheaterUI, cheaterLabelPosition, Quaternion.identity);
        trashObjects.Add(cheaterLabel);
        Vector2 lightningPosition = earlyPlayer.transform.position;
        lightningPosition.y += 4.2f;
        CreateLightning(Lightning, lightningPosition);
        earlyPlayer.GetComponent<PlayerAnimator>().EarlyShot();
        StopCoroutine(startGameCoroutineInstance);
        StartCoroutine(EndGame());
    }

    protected virtual IEnumerator EndGame()
    {
        gameActive = false;
        float sadCount = 0f;
        while (sadCount <= 2)
        {
            sadCount += Time.deltaTime;
            yield return null;
        }
        this.GetComponent<PracticeMenu>().EndGame();
    }

    public static void SetGlobalLight(float intensity)
    {
        GameManager.globalLight.intensity = intensity;
    }
    
    public static void SetGlobalLight(float intensity, Color color)
    {
        GameManager.globalLight.intensity = intensity;
        GameManager.globalLight.color = color;
    }
    protected IEnumerator RunTimer()
    {
        timer.gameObject.SetActive(true);
        float time = 0.0f;
        while (gameActive == true)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("F2");
            yield return null;
        }
    }
}
