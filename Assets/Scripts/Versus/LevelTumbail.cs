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

    public void CreateBackground()
    {
        GameObject newBackground = Instantiate(this.gameObject, new Vector3(0, 0, 1), Quaternion.identity);
        newBackground.transform.localScale = new Vector2(1f, 1f);
        newBackground.transform.SetParent(GameObject.Find("Background").transform);
        //newBackground.transform.position = new Vector3(0, 0, 1);
    }
}
