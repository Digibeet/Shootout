using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachLevelManager : LevelManager
{
    [SerializeField] private GameObject seagull;
    
    public override void PlayDuellStart()
    {
        SpawnObjects();
    }

    public override void SpawnObjects()
    {
        SpawnSeagulls(5);
    }

    private void SpawnSeagulls(int amountOfSeagulls){
    for (int i = 0; i < amountOfSeagulls; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-10, 10), Random.Range(0, 4));
            GameObject newSeagull = Instantiate(seagull, spawnPosition, Quaternion.identity);
            newSeagull.transform.parent = levelObjectsParent.transform;
        }
    }
}
