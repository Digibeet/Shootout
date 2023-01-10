using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTumbail : MonoBehaviour
{
    private void OnMouseDown()
    {
        EmptyBackground();
        CreateBackground();
    }

    private void EmptyBackground()
    {
        foreach (Transform child in GameObject.Find("Background").transform)
        {
            Destroy(child.gameObject);
        }
    }

    public GameObject CreateBackground()
    {
        GameObject newBackground = Instantiate(this.gameObject, new Vector3(0, 0, 1), Quaternion.identity);
        Destroy(newBackground.GetComponent<LevelTumbail>());
        newBackground.transform.localScale = new Vector2(1f, 1f);
        newBackground.transform.SetParent(GameObject.Find("Background").transform);
        newBackground.GetComponent<LevelManager>().InitiateLevel();
        return newBackground;
    }
}
