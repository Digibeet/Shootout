using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusManager : GameManager
{
    [SerializeField] protected GameObject player2;
    [SerializeField] private GameObject bulletUI_p1;
    [SerializeField] private GameObject bulletUI_p2;
    [SerializeField] private GameObject SceneSelectionUI;
    [SerializeField] private GameObject GameplayUI;
    private Animator player2Animator;
    private int bulletsLeft_p1 = 6;
    private int bulletsLeft_p2 = 6;

    public List<GameObject> backGrounds;


    // Start is called before the first frame update
    protected override void Start()
    {
        lost = true;
        drawStarted = false;
        endScene = "Versus";
        playerAnimator = player.GetComponent<Animator>();
        player2Animator = player2.GetComponent<Animator>();
        level = 1;

        CreateBackgroundsTumbnails();
    }

    private void CreateBackgroundsTumbnails()
    {
        List<GameObject> backgroundTumbnails = new List<GameObject>();
        Vector2 tumbnailPosition = new Vector2(-9, 2);
        for(int backgroundIndex = 0; backgroundIndex < backGrounds.Count; backgroundIndex++)
        {
            GameObject background = backGrounds[backgroundIndex];
            GameObject newTumbnail = Instantiate(background, tumbnailPosition, Quaternion.identity);
            tumbnailPosition.x = tumbnailPosition.x + 3;
            newTumbnail.transform.localScale = new Vector2(0.1f, 0.1f);
            newTumbnail.AddComponent<BoxCollider2D>();
            newTumbnail.AddComponent<LevelTumbail>();
            backgroundTumbnails.Add(newTumbnail);
        }
        backgroundTumbnails[0].GetComponent<LevelTumbail>().CreateBackground();
    }

    public void StartButton()
    {
        Debug.Log("Starting duelllll");
        lost = false;
        GameplayUI.SetActive(true);
        SceneSelectionUI.SetActive(false);
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
                        Shoot(playerAnimator);
                        bulletsLeft_p1 = ReduceBullets(bulletsLeft_p1, 1, bulletUI_p1);
                        BoxCollider2D opponentHitBox = player2.GetComponent<BoxCollider2D>();
                        if(opponentHitBox.bounds.Contains(worldPosition)){
                            player2.GetComponent<Player>().Hit();
                        }
                    }
                    else
                        NoAmmo();
                    
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
                            player.GetComponent<Player>().Hit();
                        }
                    }
                    else
                        NoAmmo();
                }
                
            }
        }
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

    protected override void StartDuel()
    {
        Debug.Log("Starting duel");
        soundManager.PlayCrows();
        playerAnimator.Play("Draw_Player");
        player2Animator.Play("Draw_Player");
    }
}
