using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MobStates;

public class ImpController : MobBase
{
    #region Serialized Fields
    #endregion

    #region Fields
    #endregion

    #region Auto Properties
    #endregion

    #region Full Properties
    #endregion

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();
        DefaultTarget = GameManager.Instance.RoundManager.Nexus.transform;
        CurrentTarget = DefaultTarget;
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.ChangeState(MoveState.Instance);
    }

    private void Update()
    {
        StateMachine.Update();
        Attack.UpdateCooldown(Time.deltaTime);
        UpdateStatus();
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void GetRandomNexusNode()
    {
        int chosenNode = Random.Range(0, GameManager.Instance.RoundManager.Nexus.NavPoints.Length);
        DefaultTarget = GameManager.Instance.RoundManager.Nexus.NavPoints[chosenNode];
        CurrentTarget = DefaultTarget;
    }

    #endregion
}
