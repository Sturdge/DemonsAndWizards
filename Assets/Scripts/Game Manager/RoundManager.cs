using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField]
    private float buildTime;

    [Header("Level Settings")]
    [SerializeField]
    private GameObject _nexus;

    public GameObject Nexus => _nexus;

    private bool isBuildRound;

    private void Awake()
    {
        isBuildRound = true;
    }

    public void StartBuildRound()
    {
        isBuildRound = true;
        StartCoroutine(BuildRoundTimer());
    }

    private IEnumerator BuildRoundTimer()
    {
        yield return new WaitForSeconds(buildTime);
        isBuildRound = false;
    }

}
