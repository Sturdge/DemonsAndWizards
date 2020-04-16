using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    mainMenu,
    characterSelect,
    inGame,
    paused
}

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public GameState GameState { get; private set; }
    public RespawnManager RespawnManager { get; private set; }
    public PlayerSetupManager SetupManager { get; private set; }
    public EntityManager EntityManager { get; private set; }
    public PlayerInputManager InputManager { get; private set; }
    public RoundManager RoundManager { get; private set; }
    public WaveManager WaveManager { get; private set; }

    public delegate void GameStateChangeHandler();
    public event GameStateChangeHandler OnStateChange;

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        else
            _instance = this;

        InputManager = GetComponent<PlayerInputManager>();
        RespawnManager = GetComponent<RespawnManager>();
        SetupManager = GetComponent<PlayerSetupManager>();
        EntityManager = GetComponent<EntityManager>();
        RoundManager = GetComponent<RoundManager>();
        WaveManager = GetComponent<WaveManager>();
    }

    private void Start()
    {
        Initialisation();
    }

    public void ChangeGameState(GameState newState)
    {
        GameState = newState;
        OnStateChange();
    }

    private void Initialisation()
    {
        //Make sure managers that aren't needed are disabled
        RespawnManager.enabled = false;
        //SetupManager.enabled = false;
        EntityManager.enabled = false;
        //WaveManager.StartSpawning();
        SetupManager.PlacePlayers();
    }

    private void OnPlayerJoined()
    {
    }
}