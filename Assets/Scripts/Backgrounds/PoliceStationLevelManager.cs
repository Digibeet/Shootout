using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStationLevelManager : LevelManager
{
    [SerializeField] GameObject lights;
    
    public override void SpawnObjects()
    {
        lights.SetActive(true);
    }
}
