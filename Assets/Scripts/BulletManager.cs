using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Sprite[] bulletImages;

    public void SetBulletImage(int bulletsLeft)
    {
        GetComponent<Image>().sprite = bulletImages[bulletsLeft];
    }
}
