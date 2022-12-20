using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbnailController : MonoBehaviour
{
    [SerializeField] private GameObject gameCharacter;
    private GameConfigurator gameConfigurator;
    private int playerSide;

    public GameObject GetCharacter()
    {
        return gameCharacter;
    }

    public void SetGameCharacter(GameObject new_gameCharacter)
    {
        gameCharacter = new_gameCharacter;
    }

    public void SetGameConfigurator(GameConfigurator new_GameConfigurator)
    {
        gameConfigurator = new_GameConfigurator;
    } 

    public void SetPlayerSide(int new_playerSide)
    {
        playerSide = new_playerSide;
    }

    private void OnMouseDown()
    {
        Debug.Log("Character portrait clicked");
        gameConfigurator.ChangeCharacter(gameCharacter, playerSide);
    }
}
