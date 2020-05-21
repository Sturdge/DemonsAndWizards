namespace StateMachine
{
    public class StateMachine<T>
    {
        public State<T> CurrentState { get; private set; }
        private readonly T parent;

        public StateMachine(T par)
        {
            parent = par;
            CurrentState = null;
        }


        public void ChangeState(State<T> newState)
        {
            if (CurrentState != null)
                CurrentState.ExitState(parent);
            CurrentState = newState;
            CurrentState.EnterState(parent);
        }

        public void Update()
        {
            if (CurrentState != null)
                CurrentState.UpdateState(parent);
        }
    }

    public abstract class State<T>
    {

        public abstract void EnterState(T parent);
        public abstract void UpdateState(T parent);
        public abstract void ExitState(T parent);
    }
}