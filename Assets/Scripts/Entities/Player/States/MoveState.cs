using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.PlayerStates
{
    public class MoveState : State<PlayerController>
    {
        #region Fields

        private float speed;
        private Vector3 movement;
        private Quaternion targetRotation;

        #endregion

        #region Auto Properties
        #endregion

        #region Full Properties

        private static MoveState _instance;
        public static MoveState Instance
        {
            get
            {

                if (_instance == null)
                    _instance = new MoveState();

                return _instance;

            }
        }

        #endregion

        #region Public Methods

        public override void EnterState(PlayerController parent)
        {
            //Acceleration and animation?
        }

        public override void ExitState(PlayerController parent)
        {
            //Decceleration and animation?
        }

        public override void UpdateState(PlayerController parent)
        {
            MovementAndRotation(parent);
            parent.Shooting();
        }

        #endregion

        #region Private Methods

        private void MovementAndRotation(PlayerController parent)
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

        #endregion
    }
}