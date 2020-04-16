using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private float respawnTime;

    public Transform[] SpawnPoints { get { return _spawnPoints; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Respawn(PlayerController entity)
    {
            entity.gameObject.SetActive(false);
            entity.StatusEffects.Clear();
            entity.transform.position = SpawnPoints[entity.PlayerID].position;
            yield return new WaitForSeconds(respawnTime);
            entity.gameObject.SetActive(true);
            GameManager.Instance.EntityManager.OnPlayerRespawn(entity);
    }

}