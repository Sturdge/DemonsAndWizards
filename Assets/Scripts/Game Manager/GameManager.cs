using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    private CameraController mainCamera;
    private RespawnManager respawnManager;
    private PlayerSetupManager setupManager;

    public int PlayerID { get; private set; }
    public int ActivePlayers { get; private set; }
    public List<GameObject> Players { get; private set; }
    public PlayerInputManager inputManager{get; private set; }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        else
            _instance = this;

        Players = new List<GameObject>();
        inputManager = GetComponent<PlayerInputManager>();
        respawnManager = GetComponent<RespawnManager>();
        setupManager = GetComponent<PlayerSetupManager>();
    }

    private void Start()
    {
        PlayerID = 0;
        ActivePlayers = 0;
        setupManager.PlacePlayers();
    }

    private void OnPlayerJoined()
    {
    }

    public void OnEntityDie(Entity entity)
    {
    }

    public void OnPlayerDie(PlayerController player)
    {
        ActivePlayers--;
        mainCamera.activePlayers.Remove(player.gameObject);
        StartCoroutine(respawnManager.Respawn(player));
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
        inputManager.playerPrefab.transform.position = respawnManager.SpawnPoints[PlayerID].position;
        PlayerID++;
    }
}