using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private Transform[] spawners;
    [SerializeField]
    private float groupSpawnDelay;
    [SerializeField]
    private float entitySpawnDelay;

    private int activeSpawners;

    public int CurrentWave { get; private set; }

    private void Initialisation()
    {
        CurrentWave = 0;
        activeSpawners = 4;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnTimer());
        CurrentWave++;

        if (CurrentWave % 10 == 0 && activeSpawners < spawners.Length)
            activeSpawners++;
    }

    private IEnumerator SpawnTimer()
    {
        Wave current = waves[CurrentWave];

        for (int i = 0; i < current.WaveData.Length; i++)
        {
            yield return new WaitForSeconds(groupSpawnDelay);

            for (int spawner = 0; spawner < spawners.Length; spawner++)
            {
                for (int entity = 0; entity < current.WaveData[i].Number; entity++)
                {
                    Instantiate(current.WaveData[i].Prefab, spawners[spawner].position, spawners[spawner].rotation);

                    yield return new WaitForSeconds(entitySpawnDelay);
                }
            }
        }
    }
}
