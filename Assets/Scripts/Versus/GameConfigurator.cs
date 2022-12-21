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
            SpawnCharacterTumbnail(characters[characterIndex], tumbnailPosition, 1);
            tumbnailPosition.x += 3;
        }
    }

    private void SpawnCharacterTumbnail(GameObject character, Vector2 tumbnailPosition, int playerSide)
    {
        PlayerAnimator characterInfo = character.GetComponent<PlayerAnimator>();
        Sprite tumbnail = characterInfo.characterTumbnail;
        string characterName = characterInfo.character_name;
        Debug.Log("Spawning tumbnail with name " + characterName);
        GameObject newTumbnail = new GameObject(characterName);
        newTumbnail.transform.localScale = new Vector2(0.5f, 0.5f);
        newTumbnail.transform.position = tumbnailPosition;
        newTumbnail.AddComponent<TumbnailController>().Init(character, this, playerSide);
        newTumbnail.AddComponent<SpriteRenderer>().sprite = tumbnail;
        newTumbnail.AddComponent<BoxCollider2D>();
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
        GameObject new_player = Instantiate(new_character, new Vector2(0, 0), Quaternion.identity);
        if(playerSide == 1)
        {
            player1 = SwitchCharacter(new_player, player1);     
        }
        else
        {
            player2 = SwitchCharacter(new_player, player2);
        }
    }

    private GameObject SwitchCharacter(GameObject newPlayer, GameObject oldPlayer)
    {
        newPlayer.transform.position = oldPlayer.transform.position;
        newPlayer.transform.localScale = oldPlayer.transform.localScale;
        Destroy(oldPlayer);
        return newPlayer;
    }
}
