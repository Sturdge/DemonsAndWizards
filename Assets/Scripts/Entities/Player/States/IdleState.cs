using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<PlayerController>
{
    private static IdleState _instance;
    public static IdleState Instance => _instance;
    public IdleState(PlayerController par) : base(par)
    {
        if (_instance == null)
            _instance = this;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        parent.Shooting();
    }
}