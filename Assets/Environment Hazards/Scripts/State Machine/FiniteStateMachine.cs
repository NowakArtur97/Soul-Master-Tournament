public class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    public FiniteStateMachine(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
