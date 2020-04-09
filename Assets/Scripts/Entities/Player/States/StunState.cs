using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State<PlayerController>
{
    public StunState(PlayerController par) : base(par) { }

    public override void EnterState()
    {
        parent.StartCoroutine(StunTimer());
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(3);
        parent.StateMachine.ChangeState(parent.States[PlayerStates.idle]);
    }
}