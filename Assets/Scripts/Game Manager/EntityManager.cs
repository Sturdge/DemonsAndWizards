using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    [SerializeField]
    private CameraController mainCamera = null;

    public int PlayerID { get; private set; }
    public int ActivePlayers { get; private set; }
    public List<PlayerController> Players { get; private set; }
    public List<GameObject> Enemies { get; private set; }

    private GameManager gameManager;

    private int aliveEnemies;
    private float delay;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        Enemies = new List<GameObject>();
        Players = new List<PlayerController>();
    }

    public void Initialisation()
    {
        PlayerID = 0;
        ActivePlayers = 0;
        aliveEnemies = 0;
    }

    public void OnEntitySpawn(GameObject mob)
    {
        Enemies.Add(mob);
        aliveEnemies++;
    }

    public void OnEntityDie()
    {
        aliveEnemies--;
        for(int i = 0; i < Players.Count; i++)
        {
            Players[i].ModifiyMoney(10);
        }
        if (aliveEnemies == 0)
        {
            gameManager.RoundManager.StartBuildRound();
            delay = gameManager.RoundManager.BuildTime / Enemies.Count;
            StartCoroutine(CleanUpMobs());
        }
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
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i< temp.Length; i++)
        {
            Players.Add(temp[i].GetComponent<PlayerController>());
        }
        mainCamera.PopulateList(Players);
        PlayerID++;
    }

    private IEnumerator CleanUpMobs()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Destroy(Enemies[i]);
            yield return new WaitForSeconds(delay);
        }
        Enemies.Clear();
    }
}
