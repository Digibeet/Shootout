using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAnimalAnimator : AnimalAnimator
{
    protected  IEnumerator MoveAnimalCoroutine()
    {
        while (true)
        {
            Vector2 newPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0.0f, 4.0f));
            //if new position is left of current position, flip the sprite
            if (newPosition.x < this.transform.position.x)
            {
                this.transform.localScale = new Vector3(-2, 2);
            }
            else
            {
                this.transform.localScale = new Vector3(2, 2);
            }
            float speed = 1.0f;
            while (Vector3.Distance(this.transform.position, newPosition) > 0.1f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, newPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    //Start and replay the Seagull_flying animation x times and then wait for y seconds and repeat
    protected IEnumerator FlapWings()
    {
        while (true)
        {
            int numberOfTimes = Random.Range(1, 6);
            for (int i = 0; i < numberOfTimes; i++)
            {
                Debug.Log(i);
                animalAnimator.Play("Seagull_flying", -1, 0f);
                float flyingAnimationLength = animalAnimator.GetCurrentAnimatorClipInfo(0).Length;
                Debug.Log(flyingAnimationLength);
                yield return new WaitForSeconds(flyingAnimationLength);
            }
            yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));
        }
    }

    protected override void MoveAnimal()
    {
        StartCoroutine(MoveAnimalCoroutine());
        StartCoroutine(FlapWings());
    }

    protected override void PlayAnimalSounds()
    {
        
    }
}
