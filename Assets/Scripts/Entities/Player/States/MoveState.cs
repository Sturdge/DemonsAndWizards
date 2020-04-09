using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State<PlayerController>
{

    private float speed;
    private Vector3 movement;
    private Quaternion targetRotation;

    public MoveState(PlayerController par) : base(par) { }

    public override void EnterState()
    {
        //Acceleration and animation?
    }

    public override void ExitState()
    {
        //Decceleration and animation?
    }

    public override void UpdateState()
    {
        MovementAndRotation();
        parent.Shooting();
    }

    private void MovementAndRotation()
    {
        movement = new Vector3
        {
            x = parent.MovementInput.x,
            z = parent.MovementInput.y
        };

        if (movement != Vector3.zero)
            targetRotation = Quaternion.LookRotation(movement);

        if (parent.IsCharging)
        {
            speed = parent.EntityData.BaseSpeed * parent.ChargeMultiplier * parent.MovementMultiplier;
        }
        else
            speed = parent.EntityData.BaseSpeed * parent.MovementMultiplier;

        parent.PlayerMovementController.Move(movement * speed * Time.deltaTime);
        if (!parent.IsStrafing)
            parent.transform.rotation = targetRotation;
    }
}