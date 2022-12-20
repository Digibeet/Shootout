using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfigurator : MonoBehaviour
{
    public List<GameObject> backGrounds;
    public List<GameObject> characters;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject sceneSelectionUI;
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
        player2 = Instantiate(characters[1], new Vector2(6, -4), Quaternion.identity);
        player2.transform.localScale = new Vector3(-player2.transform.localScale.x,player2.transform.localScale.y,1);
        CreateCharacterTumbnails();
    }

    private void CreateCharacterTumbnails()
    {
        Vector2 tumbnailPosition = new Vector2(-9, 2);
        Transform tumbnailParent = sceneSelectionUI.transform.Find("CharacterTumbnails");
        for (int characterIndex = 0; characterIndex < characters.Count; characterIndex++)
        {
            GameObject character = characters[characterIndex];
            PlayerAnimator characterInfo = character.GetComponent<PlayerAnimator>();
            Sprite tumbnail = characterInfo.characterTumbnail;
            string characterName = characterInfo.character_name;
            Debug.Log("Spawning tumbnail with name " + characterName);
            GameObject newTumbnail = new GameObject(characterName);
            newTumbnail.transform.localScale = new Vector2(0.5f, 0.5f);
            newTumbnail.transform.position = tumbnailPosition;
            TumbnailController new_controller = newTumbnail.AddComponent<TumbnailController>();
            new_controller.SetGameConfigurator(this);
            new_controller.SetGameCharacter(character);
            new_controller.SetPlayerSide(1);
            newTumbnail.AddComponent<SpriteRenderer>();
            newTumbnail.GetComponent<SpriteRenderer>().sprite = tumbnail;
            newTumbnail.AddComponent<BoxCollider2D>();
            tumbnailPosition.x = tumbnailPosition.x + 3;
            //newTumbnail.transform.parent = tumbnailParent;
        }
    }

    private void CreateBackgroundsTumbnails()
    {
        Transform tumbnailParent = sceneSelectionUI.transform.Find("LevelTumbnails");
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
            newTumbnail.transform.parent = tumbnailParent;
            backgroundTumbnails.Add(newTumbnail);
        }
        backgroundTumbnails[0].GetComponent<LevelTumbail>().CreateBackground();
    }

    public void StartButton()
    {
        Debug.Log("Starting duelllll");
        sceneSelectionUI.SetActive(false);
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

    public void ChangeCharacter(GameObject new_character, int playerSide)
    {
        GameObject playerToChange;
        Vector2 newSpawnLocation;
        Vector3 newScale;
        if(playerSide == 1)
        {
            playerToChange = player1;     
        }
        else
        {
            playerToChange = player2;
        }
        newSpawnLocation = playerToChange.transform.position;
        newScale = playerToChange.transform.localScale;
        Destroy(playerToChange);
        GameObject new_player = Instantiate(new_character, newSpawnLocation, Quaternion.identity);
        new_player.transform.localScale = newScale;
    }
}
