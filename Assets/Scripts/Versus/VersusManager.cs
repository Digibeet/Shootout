using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VersusManager : GameManager
{
    protected GameObject player2;
    [SerializeField] private List<GameObject> bulletUI_p2;
    [SerializeField] private GameObject gameConfigurator;
    private GameConfigurator versusGameConfigurator;

    private PlayerAnimator player1Animator;
    private PlayerAnimator player2Animator;
    private int bulletsLeft_p2 = maxBullets;

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
        gameActive = false;
        drawStarted = false;
        timer.text = "0";
        PrintScore(1);
        PrintScore(2);
        bulletsLeft_p1 = maxBullets;
        ResetBullets(bulletUI_p1);
        bulletsLeft_p2 = maxBullets;
        ResetBullets(bulletUI_p2);
        timer.gameObject.SetActive(false);
    }

    public void Restart()
    {
        foreach(GameObject obj in trashObjects)
        {
            Destroy(obj);
        }
        InitializeLevel();
        StartGame();
    }

    protected override void Update()
    {
        if (gameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                if (worldPosition.x > 0)
                {
                    if (!drawStarted)
                    {
                        EarlyShot(player, 1);
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
                        EarlyShot(player2, 2);
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

    protected void ShootPlayer(Player player)
    {
        if (player.Hit())
        {
            StartCoroutine(EndGame());
        }
    }

    protected override IEnumerator EndGame()
    {
        Debug.Log("Game concluded");
        gameActive = false;
        float deathCount = 0f;
        while (deathCount <= 4)
        {
            deathCount += Time.deltaTime;
            yield return null;
        }
        versusGameConfigurator.Victory();
    }

    public override void StartDuel()
    {
        gameActive = true;
        drawStarted = true;
        ScoreManager.playSound(drawStartSound);
        player1Animator.Draw();
        player2Animator.Draw();
        timer.gameObject.SetActive(true);
        StartCoroutine(RunTimer());
    }

    protected override void EarlyShot(GameObject earlyPlayer, int playerNumber)
    {
        if (playerNumber > 2 || playerNumber < 1)
        {
            Debug.LogError(playerNumber + " playerNumber for earlyshot is not a valid player");
            return;
        }
        base.EarlyShot(earlyPlayer);
        if (playerNumber == 1)
        {
            ScoreManager.IncreaseScore(2);
            PrintScore(2);
        }
        else
        {
            ScoreManager.IncreaseScore(1);
            PrintScore(1);
        }
    }

    private IEnumerator RunTimer()
    {
        float time = 0.0f;
        while(gameActive == true)
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
