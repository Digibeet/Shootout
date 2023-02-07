using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;


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

    public int level { get; protected set; }

    

    
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
        level = DifficultyManager.Instance.GetLevel();
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
                }
                else
                    NoAmmo(playerAnimator);
                
            }
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

    protected void NoAmmo(PlayerAnimator shootingPlayer)
    {
        shootingPlayer.EmptyClip();
    }

    private void IntroduceLevel()
    {
        string new_levelText = "PRACTICE ROUND " + level;
        levelText.text = new_levelText;
    }

    protected void CreateLightning(GameObject lightning, Vector2 lightningPosition)
    {
        GameObject newLightning = Instantiate(lightning, lightningPosition, Quaternion.identity);
        newLightning.GetComponent<SpriteRenderer>().enabled = true;
        newLightning.GetComponent<Animator>().Play("Flash", -1, 0);
        Destroy(newLightning, newLightning.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    public void StartGame()
    {
        gameActive = true;
        int randomClipIndex = Random.Range(0, duellStartClip.Count);
        playSound(duellStartClip[randomClipIndex]);
        startGameCoroutineInstance = StartCoroutine(StartGameCoroutine());
    }

    protected IEnumerator StartGameCoroutine()
    {      
        float startCount = 0.0f;
        float startTime = 5.0f + Random.Range(0, 1);
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
        playerAnimator.Draw();
        enemyAnimator.Draw();
        enemyDrawing = StartCoroutine(EnemyShoots());
    }

    private IEnumerator EnemyShoots()
    {
        float enemyReactionTime = 3 / level;
        float timePassed = 0.0f;
        while(timePassed < enemyReactionTime)
        {
            timePassed = Time.deltaTime;
            yield return null;
        }
        enemyAnimator.Shoot();
        playerAnimator.Die();
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
        gameActive = false;
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
        ScoreManager.playSound(earlyShotSound);
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

    public void TooLate()
    {
        playerAnimator.Die();
        Lose();
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
        this.GetComponent<PracticeMenu>().Lost();
    }

    public void ChangeLevelText(string newLevelText)
    {
        levelText.text = newLevelText;
    }

    public AudioSource playSound(AudioClip sound)
    {
        GameObject soundObject = new GameObject();
        GameObject soundParent = GameObject.Find("Sounds");
        if (soundParent == null)
        {
            soundParent = new GameObject("Sounds");
        }
        soundObject.transform.parent = soundParent.transform;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
        Destroy(soundObject, sound.length);
        return audioSource;
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
}
