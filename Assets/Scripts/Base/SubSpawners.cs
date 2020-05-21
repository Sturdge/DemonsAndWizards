using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpawners : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawners = null;

    private int spawner;

    private void Awake()
    {
        spawner = 0;
    }

    public void SpawnMob(UnityEngine.GameObject mobToSpawn)
    {
        UnityEngine.GameObject mob = Instantiate(mobToSpawn, spawners[spawner].position, spawners[spawner].rotation);
        GameManager.Instance.EntityManager.OnEntitySpawn(mob);
        spawner = (spawner + 1) % 2;
    }
}
