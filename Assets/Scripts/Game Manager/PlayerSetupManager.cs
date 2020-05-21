using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    private UnityEngine.GameObject[] characters = null;
    [SerializeField]
    private Transform[] spawnLocations = null;
    [SerializeField, Range(1, 4)]
    private int amountOfPlayers = 1;

    public void PlacePlayers()
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameManager.Instance.InputManager.playerPrefab = characters[i];
            GameManager.Instance.InputManager.playerPrefab.transform.position = spawnLocations[i].position;
            GameManager.Instance.InputManager.JoinPlayer();
        }

        GameManager.Instance.EntityManager.PopulateList();
    }
}