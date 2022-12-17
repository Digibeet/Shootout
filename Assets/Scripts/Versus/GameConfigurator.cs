using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurator : MonoBehaviour
{
    public List<GameObject> backGrounds;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject SceneSelectionUI;
    [SerializeField] private GameObject GameplayUI;
    public GameObject GameManager;
    private VersusManager versusManager;

    // Start is called before the first frame update
    void Start()
    {
        SelectCharacter();
    }

    private void SelectCharacter()
    {
        
    }

    public void CreateBackgroundsTumbnails()
    {
        List<GameObject> backgroundTumbnails = new List<GameObject>();
        Vector2 tumbnailPosition = new Vector2(-9, 2);
        for (int backgroundIndex = 0; backgroundIndex < backGrounds.Count; backgroundIndex++)
        {
            GameObject background = backGrounds[backgroundIndex];
            GameObject newTumbnail = Instantiate(background, tumbnailPosition, Quaternion.identity);
            tumbnailPosition.x = tumbnailPosition.x + 3;
            newTumbnail.transform.localScale = new Vector2(0.1f, 0.1f);
            newTumbnail.AddComponent<BoxCollider2D>();
            newTumbnail.AddComponent<LevelTumbail>();
            newTumbnail.transform.parent = SceneSelectionUI.transform;
            backgroundTumbnails.Add(newTumbnail);
        }
        backgroundTumbnails[0].GetComponent<LevelTumbail>().CreateBackground();
    }

    public void StartButton()
    {
        Debug.Log("Starting duelllll");
        SceneSelectionUI.SetActive(false);
        GameplayUI.SetActive(true);
        StartCoroutine(GameManager.GetComponent<VersusManager>().StartGame());
    }

    public void SelectSceneButton()
    {
        selectButton.SetActive(false);
        startButton.SetActive(true);
        CreateBackgroundsTumbnails();
    }
}
