using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float drawWindow;
    protected float drawTime;
    protected float shootTime = 2.5f;
    protected float lives = 1;
    [SerializeField] protected bool drawn;
    protected GameManager gameManager;

    protected IEnumerator CountdownToDraw()
    {
        float drawCount = 0f;
        while (drawCount <= drawTime)
        {
            drawCount += Time.deltaTime;
            Debug.Log("Counting down to draw" + drawCount);
            yield return null;
        }
        Draw();
    }
    public virtual void InstantiateEnemy(float new_drawWindow, float new_drawTime, GameManager new_gameManager)
    {
        drawTime = new_drawTime;
        drawWindow = new_drawWindow;
        gameManager = new_gameManager;
        drawn = false;
        StartCoroutine(CountdownToDraw());
    }

    protected void Draw()
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
            Debug.Log("Counting down shoot" + shootCount / shootTime);
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, shootCount / shootTime);
            yield return null;
        }
        Shoot();
    }

    protected void Shoot()
    {
        Debug.Log("Shoot!");
        gameManager.TooLate();
    }

    protected void killEnemy()
    {
        Destroy(gameObject);
    }

    protected virtual void Hit()
    {
        lives--;
        if (lives <= 0)
        {
            killEnemy();
        }
    }

    protected void OnMouseDown()
    {
        if (drawn)
        {
            Destroy(gameObject);
        }
        else { gameManager.EarlyShot(); }
    }
}
