using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public abstract class Enemy : MonoBehaviour
{
    protected float drawTime;
    protected float shootTime = 2.5f;
    protected float lives = 1;
    [SerializeField] protected bool drawn;
    protected GameManager gameManager;
    Animator enemyAnimator;
    private bool dead = false;
    private static bool won = false; 

    protected void Start()
    {
        enemyAnimator = this.GetComponent<Animator>();
    }

    public virtual void InstantiateEnemy(GameManager new_gameManager)
    {
        gameManager = new_gameManager;
        drawn = false;
    }

    public void Draw()
    {
        Debug.Log("Draw!");   
        enemyAnimator.Play("draw");
        drawn = true;
        StartCoroutine(CountdownToShoot());
    }
    private IEnumerator CountdownToShoot()
    {
        float shootCount = 0f;
        while (shootCount <= shootTime)
        {
            shootCount += Time.deltaTime;
            //Debug.Log("Counting down shoot" + shootCount / shootTime);
            //GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, shootCount / shootTime);
            yield return null;
        }
        if (!dead) { Shoot(); }
    }

    protected void Shoot()
    {
        Debug.Log("Shoot!");
        enemyAnimator.Play("shoot");
        won = true;
        gameManager.TooLate();
    }

    protected void KillEnemy()
    {
        Debug.Log("Enemy killed");
        dead = true;
        gameManager.CheckVictory();
        enemyAnimator.Play("die");
        //Destroy(gameObject);
    }

    protected virtual void Hit()
    {
        if (!won)
        {
            lives--;
            if (lives <= 0)
            {
                KillEnemy();
            }
        }
    }

    protected void OnMouseDown()
    {
        if (drawn)
        {
            Hit();
        }
        else { gameManager.EarlyShot(); }
    }
}
