using System.Collections;
using UnityEngine;

namespace StateMachine.PlayerStates
{
    public class StunState : State<PlayerController>
    {
        #region Fields
        #endregion

        #region Auto Properties
        #endregion

        #region Full Properties

        private static StunState _instance;
        public static StunState Instance
        {
            get
            {

                if (_instance == null)
                    _instance = new StunState();

                return _instance;

            }
        }

        #endregion

        #region Public Methods

        public override void EnterState(PlayerController parent)
        {
            parent.StartCoroutine(StunTimer(parent));
        }

        public override void ExitState(PlayerController parent)
        {

        }

        public override void UpdateState(PlayerController parent)
        {
        }

        #endregion

        #region Private Methods
        private IEnumerator StunTimer(PlayerController parent)
        {
            yield return new WaitForSeconds(3);
            parent.StateMachine.ChangeState(IdleState.Instance);
        }

        #endregion
    }
}