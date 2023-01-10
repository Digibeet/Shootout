using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusManager : GameManager
{
    protected GameObject player2;
    [SerializeField] private GameObject bulletUI_p1;
    [SerializeField] private GameObject bulletUI_p2;
    [SerializeField] private GameObject gameConfigurator;
    private GameConfigurator versusGameConfigurator;

    private PlayerAnimator player1Animator;
    private PlayerAnimator player2Animator;
    private int bulletsLeft_p1 = 6;
    private int bulletsLeft_p2 = 6;

    [SerializeField] private List<GameObject> tallies;

    protected override void Start()
    {
        lost = true;
        drawStarted = false;
        endScene = "Versus";
        level = 1;
        versusGameConfigurator = gameConfigurator.GetComponent<GameConfigurator>();
        PrintScore(1);
        PrintScore(2);
    }

    public void Restart()
    {
        lost = true;
        drawStarted = false;
        bulletsLeft_p1 = 6;
        bulletsLeft_p2 = 6;
        StartCoroutine(StartGame());
        PrintScore(1);
        PrintScore(2);
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
        Debug.Log("printing score of player " + player);
        int score = ScoreManager.GetScore(player);
        if (score == 0){
            return;
        }
        List<GameObject> scoreInTallies = ConvertScoreToTallies(score);
        if (player == 1)
        {
            Debug.Log("Destroying old score");
            Transform scoreParent = bulletUI_p1.transform.parent.Find("Score_p1");
            //Reset old score by destroying all children of scoreParent
            foreach (Transform child in scoreParent)
            {
                Destroy(child.gameObject);
            }
            for (int tallyIndex = 0; tallyIndex < scoreInTallies.Count; tallyIndex++)
            {
                GameObject currentTally = scoreInTallies[tallyIndex];
                float xPosition = -8 + tallyIndex * 0.8f;
                currentTally.transform.position = new Vector2(xPosition, -4.4f);
                currentTally.transform.parent = scoreParent;
            }
        } if(player == 2)
        {
            Transform scoreParent = bulletUI_p2.transform.parent.Find("Score_p2");
            //Reset old score by destroying all children of scoreParent
            foreach (Transform child in scoreParent)
            {
                Destroy(child.gameObject);
            }

            for (int tallyIndex = 0; tallyIndex < scoreInTallies.Count; tallyIndex++)
            {
                GameObject currentTally = scoreInTallies[tallyIndex];
                float xPosition = 8 - tallyIndex * 0.8f;
                currentTally.transform.position = new Vector2(xPosition, -4.4f);
                currentTally.transform.parent = scoreParent;
            }
        }
    }

    private List<GameObject> ConvertScoreToTallies(int score)
    {
        List<GameObject> tallyScore = new List<GameObject>();
        int amountOfFullTallies = Mathf.FloorToInt(score / 5);
        if (amountOfFullTallies > 0)
        {
            for (int i = 0; i < amountOfFullTallies; i++)
            {
                GameObject new_tally = Instantiate(tallies[4]);
                tallyScore.Add(new_tally);
            }
        }
        int rest = score % 5;
        if (rest > 0)
        {
            GameObject remainingTally = Instantiate(tallies[rest - 1]);
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
        Debug.Log("Starting duel via versusmanager");
        player1Animator.Draw();
        player2Animator.Draw();
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
