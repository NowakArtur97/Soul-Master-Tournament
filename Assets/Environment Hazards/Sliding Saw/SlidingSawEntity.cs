using UnityEngine;

public class SlidingSawEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardMoveAndDealDamageOnContactState _moveAndDealDamageOnContactStateData;
    public D_EnvironmentHazardMoveAndDealDamageOnContactState MoveAndDealDamageOnContactStateData
    {
        get { return _moveAndDealDamageOnContactStateData; }
        private set { _moveAndDealDamageOnContactStateData = value; }
    }
    [SerializeField] private Transform _obstacleCheck;
    public Transform ObstacleCheck
    {
        get { return _obstacleCheck; }
        private set { _obstacleCheck = value; }
    }

    protected override void Awake()
    {
        IdleState = new IdleForTimeState(this, "idle", IdleStateData);
        ActiveState = new MoveAndDealDamageState(this, "active", _moveAndDealDamageOnContactStateData);

        base.Awake();
    }
}
