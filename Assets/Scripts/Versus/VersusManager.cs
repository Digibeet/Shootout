using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusManager : GameManager
{
    [SerializeField] protected GameObject player2;
    [SerializeField] private GameObject bulletUI_p1;
    [SerializeField] private GameObject bulletUI_p2;
    private Animator player2Animator;
    private int bulletsLeft_p1 = 6;
    private int bulletsLeft_p2 = 6;


    // Start is called before the first frame update
    protected override void Start()
    {
        lost = true;
        drawStarted = false;
        endScene = "Versus";
        playerAnimator = player.GetComponent<Animator>();
        player2Animator = player2.GetComponent<Animator>();
        level = 1;
    }

    public void StartButton()
    {
        Debug.Log("Starting duelllll");
        //lost = false;
        //StartCoroutine(StartGame());
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
                    if (bulletsLeft_p1 > 0)
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
                    if (!drawStarted)
                    {
                        EarlyShot();
                    }
                }
                else
                {
                    if (bulletsLeft_p2 > 0)
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
                    if (!drawStarted)
                    {
                        EarlyShot();
                    }
                }
                
            }
        }
    }

    protected override void StartDuel()
    {
        Debug.Log("Starting duel");
        soundManager.PlayCrows();
        playerAnimator.Play("Draw_Player");
        player2Animator.Play("Draw_Player");
    }
}
