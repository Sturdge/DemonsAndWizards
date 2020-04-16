using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ImpController : Entity
{

    private NavMeshAgent navMeshAgent;

    private float attackRange = 4;

    private void Awake()
    {
        Initialisation();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.speed = EntityData.BaseSpeed;
        navMeshAgent.destination = GameManager.Instance.RoundManager.Nexus.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Nexus"))
        {
            navMeshAgent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nexus"))
        {
            navMeshAgent.isStopped = false;
        }
    }

}
