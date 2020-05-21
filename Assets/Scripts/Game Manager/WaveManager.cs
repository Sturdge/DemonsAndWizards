using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField]
    private Wave[] waves = null;
    [SerializeField]
    private SubSpawners[] spawners = null;
    [SerializeField]
    private float groupSpawnDelay = 0;
    [SerializeField]
    private float entitySpawnDelay = 0;

    private GameManager gameManager;
    private int activeSpawners;
    private int currentWave;

    public int DisplayWave { get; private set; }

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void Initialisation()
    {
        currentWave = 0;
        activeSpawners = 1;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnTimer());
        currentWave++;
        DisplayWave++;
    }

    private void OnSpawningFinished()
    {
        if (currentWave == waves.Length)
        {
            activeSpawners++;
            currentWave = 0;
        }
    }

    private IEnumerator SpawnTimer()
    {
        Wave current = waves[currentWave];

        for (int i = 0; i < current.WaveData.Length; i++)
        {
            yield return new WaitForSeconds(groupSpawnDelay);

            for (int spawner = 0; spawner < activeSpawners; spawner++)
            {
                for (int entity = 0; entity < current.WaveData[i].Number; entity++)
                {
                    spawners[spawner].SpawnMob(current.WaveData[i].Prefab);
                    yield return new WaitForSeconds(entitySpawnDelay);
                }
            }
        }

        OnSpawningFinished();
    }
}
