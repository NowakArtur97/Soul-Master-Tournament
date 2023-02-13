using UnityEngine;

public class BlockState : ActiveState
{
    private D_EnvironmentHazardBlockState _blockStateData;
    private MovingWallEntity _environmentHazardEntity;

    private float _blockTime;

    public BlockState(MovingWallEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardBlockState blockStateData) : base(environmentHazardEntity, animationBoolName)
    {
        _environmentHazardEntity = environmentHazardEntity;
        _blockStateData = blockStateData;
    }

    public override void Enter()
    {
        base.Enter();

        _blockTime = Random.Range(_blockStateData.minBlockTime, _blockStateData.maxBlockTime);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StateStartTime + _blockTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_environmentHazardEntity.StopBlockingState);
        }
    }
}
