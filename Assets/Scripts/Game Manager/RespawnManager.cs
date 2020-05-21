using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints = null;
    [SerializeField]
    private float respawnTime = 0;

    private GameManager gameManager;

    public Transform[] SpawnPoints => _spawnPoints;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public IEnumerator Respawn(PlayerController entity)
    {
            entity.gameObject.SetActive(false);
            entity.StatusEffects.Clear();
            entity.transform.position = SpawnPoints[entity.PlayerID].position;
            yield return new WaitForSeconds(respawnTime);
            entity.gameObject.SetActive(true);
            gameManager.EntityManager.OnPlayerRespawn(entity);
    }

}