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
    public UIManager UIManager { get; private set; }

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
        UIManager = GetComponent<UIManager>();
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
        SetupManager.enabled = false;
        EntityManager.enabled = false;
        RoundManager.enabled = false;
        WaveManager.enabled = false;
        UIManager.enabled = false;
        OnGameStart();
    }

    public void OnGameStart()
    {
        RespawnManager.enabled = true;
        SetupManager.enabled = true;
        EntityManager.enabled = true;
        RoundManager.enabled = true;
        WaveManager.enabled = true;
        UIManager.enabled = true;
        EntityManager.Initialisation();
        WaveManager.Initialisation();
        SetupManager.PlacePlayers();
        RoundManager.StartBuildRound();
        UIManager.UpdateGoldText();
    }

    public void OnGameEnd()
    {
        RespawnManager.enabled = false;
        SetupManager.enabled = false;
        EntityManager.enabled = false;
        RoundManager.enabled = false;
        WaveManager.enabled = false;
    }

    private void OnPlayerJoined()
    {
    }
}