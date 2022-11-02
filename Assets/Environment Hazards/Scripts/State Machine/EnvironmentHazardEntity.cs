using UnityEngine;

public class EnvironmentHazardEntity : MonoBehaviour
{
    [SerializeField] private D_EnvironmentHazardWaitState _waitStateData;
    public D_EnvironmentHazardWaitState WaitStateData { get { return _waitStateData; } private set { _waitStateData = value; } }
    [SerializeField] private D_EnvironmentHazardIdleState _idleStateData;
    public D_EnvironmentHazardIdleState IdleStateData { get { return _idleStateData; } private set { _idleStateData = value; } }

    public CoreContainer CoreContainer { get; private set; }
    public FiniteStateMachine StateMachine { get; private set; }

    public WaitState WaitState { get; protected set; }
    public IdleState IdleState { get; protected set; }
    public ActiveState ActiveState { get; protected set; }

    public GameObject ToInteract { get; private set; }

    protected virtual void Awake()
    {
        CoreContainer = GetComponentInChildren<CoreContainer>();
        IdleState = new IdleState(this, "idle", _idleStateData);
        WaitState = new WaitState(this, "wait", _waitStateData, IdleState);
    }

    private void Start() => StateMachine = new FiniteStateMachine(WaitState);

    private void Update() => StateMachine.CurrentState.LogicUpdate();

    private void OnTriggerEnter2D(Collider2D collision) => ToInteract = collision.gameObject;

    private void OnTriggerExit2D(Collider2D collision) => ToInteract = null;
}
