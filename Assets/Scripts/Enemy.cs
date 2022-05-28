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

    public void Draw()
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
            //Debug.Log("Counting down shoot" + shootCount / shootTime);
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, shootCount / shootTime);
            yield return null;
        }
        Shoot();
    }
    public void StartDraw(float randomTimeNoise)
    {
        StartCoroutine(CountdownToDraw(randomTimeNoise));
    }

    private IEnumerator CountdownToDraw(float randomTimeNoise)
    {
        float drawCount = 0f;
        float randomizedDrawTime = drawTime + Random.Range(0, randomTimeNoise);
        while (drawCount <= randomizedDrawTime)
        {
            drawCount += Time.deltaTime;
            //Debug.Log("Counting down draw " + drawCount);
            yield return null;
        }
        Draw();
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
