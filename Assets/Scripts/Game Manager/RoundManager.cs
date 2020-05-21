using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    #region Serialized Fields

    [Header("Round Settings")]
    [SerializeField]
    private float _buildTime = 0;

    [Header("Level Settings")]
    [SerializeField]
    private Nexus _nexus = null;

    #endregion

    #region Fields

    private GameManager gameManager;

    #endregion

    #region Auto Properties
    #endregion

    #region Full Properties

    public Nexus Nexus => _nexus;
    public float BuildTime => _buildTime;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    #endregion

    #region Public Methods
    public void StartBuildRound()
    {
        StartCoroutine(BuildRoundTimer());
    }

    public void OnBuildRoundEnd()
    {
        gameManager.WaveManager.StartSpawning();
    }

    #endregion

    #region Private Methods

    private IEnumerator BuildRoundTimer()
    {
        yield return new WaitForSeconds(_buildTime);
        OnBuildRoundEnd();
    }

    #endregion
}
