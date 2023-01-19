using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStationLevelManager : LevelManager
{
    public override void SetGlobalLight(float intensity)
    {
        base.SetGlobalLight(0.2f);
    }
}
