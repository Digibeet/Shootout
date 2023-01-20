using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VersusManager : GameManager
{
    protected GameObject player2;
    [SerializeField] private GameObject bulletUI_p1;
    [SerializeField] private GameObject bulletUI_p2;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject gameConfigurator;
    private GameConfigurator versusGameConfigurator;

    private PlayerAnimator player1Animator;
    private PlayerAnimator player2Animator;
    private int bulletsLeft_p1 = 6;
    private int bulletsLeft_p2 = 6;

    [SerializeField] private List<GameObject> tallies;

    protected override void Start()
    {
        level = 1;
        globalLight = lightObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        versusGameConfigurator = gameConfigurator.GetComponent<GameConfigurator>();
        SetGlobalLight(1, Color.white);
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        lost = true;
        drawStarted = false;
        timer.text = "0";
        PrintScore(1);
        PrintScore(2);
        bulletsLeft_p1 = 6;
        bulletsLeft_p2 = 6;
        timer.gameObject.SetActive(false);
    }

    public void Restart()
    {
        InitializeLevel();
        StartCoroutine(StartGame());
    }

    protected override void Update()
    {
        if (!lost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                if (worldPosition.x > 0)
                {
                    if (!drawStarted)
                    {
                        EarlyShot(player);
                    }
                    else if (bulletsLeft_p1 > 0)
                    {
                        Shoot(player1Animator);
                        bulletsLeft_p1 = ReduceBullets(bulletsLeft_p1, 1, bulletUI_p1);
                        BoxCollider2D opponentHitBox = player2.GetComponent<BoxCollider2D>();
                        if(opponentHitBox.bounds.Contains(worldPosition))
                        {
                            ScoreManager.IncreaseScore(1);
                            PrintScore(1);
                            ShootPlayer(player2.GetComponent<Player>());
                        }
                    }
                    else
                        NoAmmo(player1Animator);
                    
                }
                else
                {
                    if (!drawStarted)
                    {
                        EarlyShot(player2);
                    }
                    else if (bulletsLeft_p2 > 0)
                    {
                        Shoot(player2Animator);
                        bulletsLeft_p2 = ReduceBullets(bulletsLeft_p2, 1, bulletUI_p2);
                        BoxCollider2D opponentHitBox = player.GetComponent<BoxCollider2D>();
                        if (opponentHitBox.bounds.Contains(worldPosition))
                        {
                            ScoreManager.IncreaseScore(2);
                            PrintScore(2);
                            ShootPlayer(player.GetComponent<Player>());
                        }
                    }
                    else
                        NoAmmo(player2Animator);
                }
                
            }
        }
    }

    private void PrintScore(int player)
    {
        int score = ScoreManager.GetScore(player);
        if (score == 0 || player > 2 || player < 1){
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
        } else
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

    protected void ShootPlayer(Player player)
    {
        if (player.Hit())
        {
            StartCoroutine(EndGame());
        }
    }

    protected override IEnumerator EndGame()
    {
        lost = true;
        float deathCount = 0f;
        while (deathCount <= 4)
        {
            deathCount += Time.deltaTime;
            yield return null;
        }
        versusGameConfigurator.Victory();
    }

    public void EarlyShot(GameObject earlyPlayer)
    {
        Vector2 lightningPosition = earlyPlayer.transform.position;
        lightningPosition.y += 2.7f;
        CreateLightning(Lightning, lightningPosition);
        earlyPlayer.GetComponent<Animator>().Play("burn");
        Feedback.enabled = true;
        Lose();
    }


    public override void StartDuel()
    {
        lost = false;
        drawStarted = true;
        ScoreManager.playSound(drawStartSound);
        player1Animator.Draw();
        player2Animator.Draw();
        timer.gameObject.SetActive(true);
        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        float time = 0.0f;
        while(lost == false)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("F2");
            yield return null;
        }
    }

    public void SetPlayers(GameObject new_player1, GameObject new_player2)
    {
        player = new_player1;
        player2 = new_player2;
        player1Animator = player.GetComponent<PlayerAnimator>();
        player2Animator = player2.GetComponent<PlayerAnimator>();
        print("setting players");
    }
}
