using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    private GameObject[] characters;
    [SerializeField]
    private Transform[] spawnLocations;
    [SerializeField, Range(1, 4)]
    private int amountOfPlayers;

    private void Awake()
    {
    }

    public void PlacePlayers()
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameManager.Instance.inputManager.playerPrefab = characters[i];
            GameManager.Instance.inputManager.playerPrefab.transform.position = spawnLocations[i].position;
            GameManager.Instance.inputManager.JoinPlayer();
        }

        GameManager.Instance.PopulateList();
    }
}