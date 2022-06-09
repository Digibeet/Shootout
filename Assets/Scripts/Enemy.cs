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

    public virtual void InstantiateEnemy(GameManager new_gameManager)
    {
        gameManager = new_gameManager;
        drawn = false;
    }

    public virtual void Draw()
    {
        Debug.Log("Draw!");
        drawn = true;
        StartCoroutine(CountdownToShoot());
    }
    private IEnumerator CountdownToShoot()
    {
        float shootCount = 0f;
        while (shootCount <= shootTime)
        {
            shootCount += Time.deltaTime;
            yield return null;
        }
        Shoot();
    }

    protected virtual void Shoot()
    {
        Debug.Log("Shoot!");
        gameManager.TooLate();
    }

    protected void KillEnemy()
    {
        Debug.Log("Enemy killed");
        gameManager.CheckVictory();
        Destroy(gameObject);
    }

    protected virtual void Hit()
    {
        lives--;
        if (lives <= 0)
        {
            KillEnemy();
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
