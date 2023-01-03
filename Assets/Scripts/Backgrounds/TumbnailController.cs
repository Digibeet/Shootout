using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbnailController : MonoBehaviour
{
    [SerializeField] private GameObject gameCharacter;
    private GameConfigurator gameConfigurator;
    private int playerSide;

    public void Init(GameObject new_gameCharacter, GameConfigurator new_GameConfigurator, int new_playerSide)
    {
        gameCharacter = new_gameCharacter;
        gameConfigurator = new_GameConfigurator;
        playerSide = new_playerSide;
    }

    public GameObject GetCharacter()
    {
        return gameCharacter;
    }

    private void OnMouseDown()
    {
        Debug.Log("Character portrait clicked");
        gameConfigurator.ChangeCharacter(gameCharacter, playerSide);
    }
}
