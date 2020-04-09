using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<PlayerController>
{
    public IdleState(PlayerController par) : base(par) { }

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