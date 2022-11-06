using UnityEngine;

public abstract class IdleState : State
{
    protected D_EnvironmentHazardIdleState IdleStateData;

    public IdleState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName, D_EnvironmentHazardIdleState idleStateData)
        : base(environmentHazardEntity, animationBoolName) => IdleStateData = idleStateData;

    protected float IdleTime { get; private set; }

    public override void Enter()
    {
        base.Enter();

        IdleTime = Random.Range(IdleStateData.minIdleTime, IdleStateData.maxIdleTime);
    }
}
