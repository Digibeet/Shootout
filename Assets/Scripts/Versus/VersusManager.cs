using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusManager : GameManager
{
    [SerializeField] protected GameObject player2;
    [SerializeField] private GameObject bulletUI_p1;
    [SerializeField] private GameObject bulletUI_p2;

    private PlayerAnimator player1Animator;
    private PlayerAnimator player2Animator;
    private int bulletsLeft_p1 = 6;
    private int bulletsLeft_p2 = 6;


    // Start is called before the first frame update
    protected override void Start()
    {
        lost = true;
        drawStarted = false;
        endScene = "Versus";
        level = 1;
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

    public override void StartDuel()
    {
        lost = false;
        Debug.Log("Starting duel");
        soundManager.PlayCrows();
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
