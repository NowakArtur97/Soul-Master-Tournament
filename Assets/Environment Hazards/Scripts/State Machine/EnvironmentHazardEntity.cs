using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHazardEntity : MonoBehaviour
{
    [SerializeField] private string _environmentHazardName;

    [SerializeField] private D_EnvironmentHazardWaitState _waitStateData;
    public D_EnvironmentHazardWaitState WaitStateData { get { return _waitStateData; } private set { _waitStateData = value; } }
    [SerializeField] private D_EnvironmentHazardIdleState _idleStateData;
    public D_EnvironmentHazardIdleState IdleStateData { get { return _idleStateData; } private set { _idleStateData = value; } }

    public CoreContainer CoreContainer { get; private set; }
    public FiniteStateMachine StateMachine { get; private set; }

    public WaitState WaitState { get; protected set; }
    public IdleState IdleState { get; protected set; }
    public ActiveState ActiveState { get; protected set; }

    public List<GameObject> ToInteract { get; private set; }

    protected virtual void Awake()
    {
        ToInteract = new List<GameObject>();
        CoreContainer = GetComponentInChildren<CoreContainer>();
        _environmentHazardName = _environmentHazardName.Equals("") ? GetType().Name.Replace("Entity", "") : _environmentHazardName;
        WaitState = new WaitState(this, "wait", _waitStateData, IdleState);
    }

    private void Start()
    {
        CoreContainer.Sounds?.SetName(_environmentHazardName);
        StateMachine = new FiniteStateMachine(WaitState);
    }

    private void Update() => StateMachine.CurrentState.LogicUpdate();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (!ToInteract.Contains(collisionGameObject))
        {
            ToInteract.Add(collisionGameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (ToInteract.Contains(collisionGameObject))
        {
            ToInteract.Remove(collisionGameObject);
            Debug.Log(collisionGameObject.name);
        }
    }
}
