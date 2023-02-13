using UnityEngine;

public class MovingWallEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardBlockState _blockStateData;
    public D_EnvironmentHazardBlockState BlockStateData
    {
        get { return _blockStateData; }
        private set { BlockStateData = value; }
    }

    public StartBlockingState StartBlockingState { get; private set; }
    public StopBlockingState StopBlockingState { get; private set; }

    public BoxCollider2D MyBoxCollider2D { get; private set; }

    protected override void Awake()
    {
        StartBlockingState = new StartBlockingState(this, "startBlock");
        IdleState = new IdleForTimeBeforeNextState(this, "idle", IdleStateData, StartBlockingState);
        StopBlockingState = new StopBlockingState(this, "stopBlock");
        ActiveState = new BlockState(this, "active", _blockStateData);

        MyBoxCollider2D.enabled = false;

        base.Awake();
    }
}