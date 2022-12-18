using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurator : MonoBehaviour
{
    public List<GameObject> backGrounds;
    public List<GameObject> characters;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject SceneSelectionUI;
    [SerializeField] private GameObject GameplayUI;
    public GameObject GameManager;

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        SelectCharacter();
    }

    private void SelectCharacter()
    {
        player1 = Instantiate(characters[0], new Vector2(-6,-4), Quaternion.identity);
        player2 = Instantiate(characters[0], new Vector2(6, -4), Quaternion.identity);
        player2.transform.localScale = new Vector3(-player2.transform.localScale.x,player2.transform.localScale.y,1);
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
        GameManager.GetComponent<VersusManager>().SetPlayers(player1, player2);
        selectButton.SetActive(false);
        startButton.SetActive(true);
        CreateBackgroundsTumbnails();
    }
}
