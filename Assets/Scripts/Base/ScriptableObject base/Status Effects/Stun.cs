using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStun", menuName = "Status Effects/Stun")]
public class Stun : StatusEffect
{
    private PlayerController parentPlayerController = null;

    public override void OnStart(Entity parent)
    {
        base.OnStart(parent);
        parentPlayerController.StateMachine.ChangeState(parentPlayerController.States[PlayerStates.stun]);
    }

    public override void DoStatusEffect(float deltaTime)
    {
        base.DoStatusEffect(deltaTime);
    }

    public override void OnEnd()
    {
        parentPlayerController.StateMachine.ChangeState(parentPlayerController.States[PlayerStates.idle]);
        base.OnEnd();
    }
}