using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConfigurator : MonoBehaviour
{
    public List<GameObject> backGrounds;
    public List<GameObject> characters;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject sceneSelectionUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject gameplayUI;
    
    [SerializeField] private GameObject characterTumbnails;
    public GameObject GameManager;

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        gameplayUI.SetActive(false);
        victoryUI.SetActive(false);
        startButton.SetActive(false);
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
        Vector2 tumbnailPositionForPlayer1 = new Vector3(-9, 2,-1);
        Vector2 tumbnailPositionForPlayer2 = new Vector3(3, 2,-1);
        for (int characterIndex = 0; characterIndex < characters.Count; characterIndex++)
        {
            if (characterIndex % 3 == 0 && characterIndex != 0)
            {
                tumbnailPositionForPlayer1.y -= 2;
                tumbnailPositionForPlayer1.x = -9;
                tumbnailPositionForPlayer2.y -= 2;
                tumbnailPositionForPlayer2.x = 3;
            }
            SpawnCharacterTumbnail(characters[characterIndex], tumbnailPositionForPlayer1, 1);
            tumbnailPositionForPlayer1.x += 2;
            SpawnCharacterTumbnail(characters[characterIndex], tumbnailPositionForPlayer2, 2);
            tumbnailPositionForPlayer2.x += 2;         
        }
    }

    private GameObject SpawnCharacterTumbnail(GameObject character, Vector2 tumbnailPosition, int playerSide)
    {
        PlayerAnimator characterInfo = character.GetComponent<PlayerAnimator>();
        Sprite tumbnail = characterInfo.characterTumbnail;
        string characterName = characterInfo.character_name;
        GameObject newTumbnail = new GameObject(characterName);
        newTumbnail.transform.localScale = new Vector2(0.3f, 0.3f);
        newTumbnail.transform.position = tumbnailPosition;
        newTumbnail.AddComponent<TumbnailController>().Init(character, this, playerSide);
        newTumbnail.AddComponent<SpriteRenderer>().sprite = tumbnail;
        newTumbnail.AddComponent<BoxCollider2D>();
        newTumbnail.transform.parent = characterTumbnails.transform;
        return newTumbnail;
    }

    private void CreateBackgroundsTumbnails()
    {
        Transform tumbnailParent = sceneSelectionUI.transform.Find("LevelTumbnails");
        List<GameObject> backgroundTumbnails = new List<GameObject>();
        Vector2 tumbnailPosition = new Vector3(-9, 2,-1);
        for (int backgroundIndex = 0; backgroundIndex < backGrounds.Count; backgroundIndex++)
        {
            //after 7 tumbnails, move to next row
            if (backgroundIndex % 7 == 0 && backgroundIndex != 0)
            {
                tumbnailPosition.y -= 3;
                tumbnailPosition.x = -9;
            }
            GameObject background = backGrounds[backgroundIndex];
            GameObject newTumbnail = Instantiate(background, tumbnailPosition, Quaternion.identity);
            Vector3 currentLocalPosition = newTumbnail.transform.localPosition;
            Vector3 newLocalPosition = new Vector3(currentLocalPosition.x, currentLocalPosition.y, -1);
            newTumbnail.transform.localPosition = newLocalPosition;
            tumbnailPosition.x = tumbnailPosition.x + 3;
            newTumbnail.transform.localScale = new Vector3(0.1f, 0.1f,-1.0f);
            newTumbnail.AddComponent<BoxCollider2D>();
            newTumbnail.AddComponent<LevelTumbail>();
            newTumbnail.transform.parent = tumbnailParent;
            backgroundTumbnails.Add(newTumbnail);
        }
        backgroundTumbnails[0].GetComponent<LevelTumbail>().CreateBackground();
    }

    public void StartButton()
    {
        Debug.Log("Clicking start button");
        sceneSelectionUI.SetActive(false);
        gameplayUI.SetActive(true);
        StartCoroutine(GameManager.GetComponent<VersusManager>().StartGame());
    }

    public void SelectSceneButton()
    {
        GameManager.GetComponent<VersusManager>().SetPlayers(player1, player2);
        selectButton.SetActive(false);
        startButton.SetActive(true);
        Destroy(characterTumbnails);
        CreateBackgroundsTumbnails();
    }

    public void RestartButton()
    {
        player1.GetComponent<PlayerAnimator>().ResetPlayer();
        player2.GetComponent<PlayerAnimator>().ResetPlayer();
        victoryUI.SetActive(false);
        gameplayUI.SetActive(true);
        GameManager.GetComponent<VersusManager>().Restart();
    }

    public void SelectCharacterButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
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

    public void Victory()
    {
        victoryUI.SetActive(true);
    }

        private GameObject SwitchCharacter(GameObject newPlayer, GameObject oldPlayer)
    {
        newPlayer.transform.position = oldPlayer.transform.position;
        newPlayer.transform.localScale = oldPlayer.transform.localScale;
        Destroy(oldPlayer);
        return newPlayer;
    }
}