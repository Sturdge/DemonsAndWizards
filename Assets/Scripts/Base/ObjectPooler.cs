using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _size;

    public string Name => _name;
    public GameObject Prefab => _prefab;
    public int Size => _size;
}

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private List<PoolableObject> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (PoolableObject pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.Name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 pos, Quaternion rot)
    {
        GameObject objectToSpawn = null;

        if (poolDictionary.ContainsKey(name))
        {
            objectToSpawn = poolDictionary[name].Dequeue();

            objectToSpawn.transform.position = pos;
            objectToSpawn.transform.rotation = rot;
            objectToSpawn.SetActive(true);

            poolDictionary[name].Enqueue(objectToSpawn);
        }

        return objectToSpawn;
    }
}
