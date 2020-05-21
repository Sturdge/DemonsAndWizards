namespace StateMachine.PlayerStates
{
    public class IdleState : State<PlayerController>
    {
        #region Fields
        #endregion

        #region Auto Properties
        #endregion

        #region Full Properties

        private static IdleState _instance;
        public static IdleState Instance
        {
            get
            {

                if (_instance == null)
                    _instance = new IdleState();

                return _instance;

            }
        }

        #endregion

        #region Public Methods

        public override void EnterState(PlayerController parent)
        {
        }

        public override void ExitState(PlayerController parent)
        {
        }

        public override void UpdateState(PlayerController parent)
        {
            parent.Shooting();
        }

        #endregion

        #region Private Methods
        #endregion
    }
}