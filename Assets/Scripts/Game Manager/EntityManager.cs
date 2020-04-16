using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField]
    private CameraController mainCamera;

    public int PlayerID { get; private set; }
    public int ActivePlayers { get; private set; }
    public List<GameObject> Players { get; private set; }

    private GameManager gameManager;

    private int aliveEnemies;

    private void Awake()
    {
        Players = new List<GameObject>();
        gameManager = GameManager.Instance;
        aliveEnemies = 0;
    }

    private void Initialisation()
    {
        PlayerID = 0;
        ActivePlayers = 0;
    }

    public void OnEntityDie()
    {
        aliveEnemies--;
        if (aliveEnemies == 0)
            gameManager.RoundManager.StartBuildRound();
    }

    public void OnPlayerDie(PlayerController player)
    {
        ActivePlayers--;
        mainCamera.activePlayers.Remove(player.gameObject);
        StartCoroutine(gameManager.RespawnManager.Respawn(player));
    }

    public void OnPlayerRespawn(PlayerController player)
    {
        ActivePlayers++;
        mainCamera.activePlayers.Add(player.gameObject);
    }
    
    public void PopulateList()
    {
        ActivePlayers++;
        Players = GameObject.FindGameObjectsWithTag("Player").ToList();
        mainCamera.activePlayers.Add(Players[PlayerID]);
        gameManager.InputManager.playerPrefab.transform.position = gameManager.RespawnManager.SpawnPoints[PlayerID].position;
        PlayerID++;
    }
}
