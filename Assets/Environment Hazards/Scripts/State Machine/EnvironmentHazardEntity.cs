using UnityEngine;

public class EnvironmentHazardEntity : MonoBehaviour
{
    public CoreContainer CoreContainer { get; private set; }
    public FiniteStateMachine StateMachine { get; private set; }

    public IdleState IdleState { get; protected set; }
    public ActiveState ActiveState { get; protected set; }

    public GameObject ToInteract { get; private set; }

    protected virtual void Awake()
    {
        CoreContainer = GetComponentInChildren<CoreContainer>();
        //WaitState = new IdleState(this, "wait");
        IdleState = new IdleState(this, "idle");
    }

    private void Start() => StateMachine = new FiniteStateMachine(IdleState);

    private void Update() => StateMachine.CurrentState.LogicUpdate();

    private void OnTriggerEnter2D(Collider2D collision) => ToInteract = collision.gameObject;

    private void OnTriggerExit2D(Collider2D collision) => ToInteract = null;
}
