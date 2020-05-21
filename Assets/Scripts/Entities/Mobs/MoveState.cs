using StateMachine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace MobStates
{
    public class MoveState : State<MobBase>
    {
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

        private NavMeshPath path = new NavMeshPath();

        public override void EnterState(MobBase parent)
        {
            parent.NavMeshAgent.destination = parent.CurrentTarget.position;

            parent.StartCoroutine(CheckDestinationComplete(parent));
        }

        public override void ExitState(MobBase parent)
        {
            parent.StopCoroutine(CheckDestinationComplete(parent));
        }

        public override void UpdateState(MobBase parent)
        {

        }

        private IEnumerator CheckDestinationComplete(MobBase parent)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (CheckRemainingDistance(parent))
                {
                    parent.SwitchTarget(GameManager.Instance.RoundManager.Nexus);
                    parent.StateMachine.ChangeState(AttackState.Instance);
                }
            }
        }

        private bool CheckRemainingDistance(MobBase parent)
        {

            bool result = false;
            if (parent.NavMeshAgent.remainingDistance <= parent.NavMeshAgent.stoppingDistance)
            {
                if (!parent.NavMeshAgent.hasPath || parent.NavMeshAgent.velocity.sqrMagnitude == 0f)
                    result = true;
            }

            return result;

        }
    }
}