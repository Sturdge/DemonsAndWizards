using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolableObject
{
    [SerializeField]
    private string _name = string.Empty;
    [SerializeField]
    private UnityEngine.GameObject _prefab = null;
    [SerializeField]
    private int _size = 0;

    public string Name => _name;
    public UnityEngine.GameObject Prefab => _prefab;
    public int Size => _size;
}

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private List<PoolableObject> pools = null;
    private Dictionary<string, Queue<UnityEngine.GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<UnityEngine.GameObject>>();

        foreach (PoolableObject pool in pools)
        {
            Queue<UnityEngine.GameObject> objectPool = new Queue<UnityEngine.GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                UnityEngine.GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.Name, objectPool);
        }
    }

    public UnityEngine.GameObject SpawnFromPool(string name, Transform parent)
    {
        UnityEngine.GameObject objectToSpawn = null;

        if (poolDictionary.ContainsKey(name))
        {
            objectToSpawn = poolDictionary[name].Dequeue();

            objectToSpawn.transform.position = parent.position;
            objectToSpawn.transform.rotation = parent.rotation;
            objectToSpawn.SetActive(true);

            poolDictionary[name].Enqueue(objectToSpawn);
        }

        return objectToSpawn;
    }
}
