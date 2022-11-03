using UnityEngine;

public class EnvironmentHazardEntity : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField] private D_EnvironmentHazardWaitState _waitStateData;
    public D_EnvironmentHazardWaitState WaitStateData { get { return _waitStateData; } private set { _waitStateData = value; } }
    [SerializeField] private D_EnvironmentHazardIdleState _idleStateData;
    public D_EnvironmentHazardIdleState IdleStateData { get { return _idleStateData; } private set { _idleStateData = value; } }

    public GameObject AliveGameObject { get; private set; }
    public CoreContainer CoreContainer { get; private set; }
    public FiniteStateMachine StateMachine { get; private set; }

    public WaitState WaitState { get; protected set; }
    public IdleState IdleState { get; protected set; }
    public ActiveState ActiveState { get; protected set; }

    public GameObject ToInteract { get; private set; }

    protected virtual void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        CoreContainer = AliveGameObject.GetComponent<CoreContainer>();
        WaitState = new WaitState(this, "wait", _waitStateData, IdleState);
    }

    private void Start() => StateMachine = new FiniteStateMachine(WaitState);

    private void Update() => StateMachine.CurrentState.LogicUpdate();

    private void OnTriggerEnter2D(Collider2D collision) => ToInteract = collision.gameObject;

    private void OnTriggerExit2D(Collider2D collision) => ToInteract = null;
}
