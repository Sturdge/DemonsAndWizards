using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobStates
{
    public class AttackState : State<MobBase>
    {
        private static AttackState _instance;
        public static AttackState Instance
        {
            get
            {

                if (_instance == null)
                    _instance = new AttackState();

                return _instance;

            }
        }

        public override void EnterState(MobBase parent)
        {
        }

        public override void ExitState(MobBase parent)
        {
        }

        public override void UpdateState(MobBase parent)
        {
            parent.transform.LookAt(parent.CurrentTarget);
            parent.Attack.DoAttack();
        }

    }
}