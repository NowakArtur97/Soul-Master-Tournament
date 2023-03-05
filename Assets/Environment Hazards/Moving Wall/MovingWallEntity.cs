using UnityEngine;

public class MovingWallEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardBlockState _blockStateData;
    public D_EnvironmentHazardBlockState BlockStateData
    {
        get { return _blockStateData; }
        private set { BlockStateData = value; }
    }
    [SerializeField] private D_EnvironmentHazardBlockedState _blockedStateData;
    public D_EnvironmentHazardBlockedState BlockedStateData
    {
        get { return _blockedStateData; }
        private set { BlockedStateData = value; }
    }

    public StartBlockingState StartBlockingState { get; private set; }
    public StopBlockingState StopBlockingState { get; private set; }

    public BoxCollider2D MyBoxCollider2D { get; private set; }

    protected override void Awake()
    {
        MyBoxCollider2D = GetComponentInChildren<BoxCollider2D>();
        MyBoxCollider2D.enabled = false;

        StartBlockingState = new StartBlockingState(this, "startBlocking");
        IdleState = new MovingWallIdleForTimeBeforeNextState(this, "idle", IdleStateData, StartBlockingState);
        StopBlockingState = new StopBlockingState(this, "stopBlocking");
        ActiveState = new BlockState(this, "active", _blockStateData);

        base.Awake();
    }
}