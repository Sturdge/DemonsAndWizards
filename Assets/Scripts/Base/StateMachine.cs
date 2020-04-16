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
            CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}

public abstract class State<T>
{
    protected T parent;
    public State(T par)
    {
        parent = par;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}