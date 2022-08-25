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
        drawStarted = false;
        endScene = "Versus";
        playerAnimator = player.GetComponent<Animator>();
        player2Animator = player2.GetComponent<Animator>();
        level = 1;
        StartCoroutine(StartGame());
    }

    protected override void Update()
    {
        if (!lost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (worldPosition.x > 0)
                {
                    if (bulletsLeft_p1 > 0)
                    {
                        Shoot(playerAnimator);
                        bulletsLeft_p1 = ReduceBullets(bulletsLeft_p1, 1, bulletUI_p1);
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
