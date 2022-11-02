using UnityEngine;

public class EnvironmentHazardEntity : MonoBehaviour
{
    public CoreContainer CoreContainer { get; internal set; }
    public FiniteStateMachine StateMachine { get; internal set; }

    public IdleState IdleState { get; private set; }
    //public ActiveState ActiveState { get; private set; }

    private void Awake()
    {
        CoreContainer = GetComponentInChildren<CoreContainer>();
        //WaitState = new IdleState(this, "wait");
        IdleState = new IdleState(this, "idle");
        //ActiveState = new ActiveState(this, "active");
    }

    private void Start() => StateMachine = new FiniteStateMachine(IdleState);

    private void Update() => StateMachine.CurrentState.LogicUpdate();
}
