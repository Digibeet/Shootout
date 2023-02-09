using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletButton : MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    public void PlaySound()
    {
        Debug.Log("CLICK");
        ScoreManager.PlaySound(shotSound);
    }
}
