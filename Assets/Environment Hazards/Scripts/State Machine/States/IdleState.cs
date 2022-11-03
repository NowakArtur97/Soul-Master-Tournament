public abstract class IdleState : State
{
    protected D_EnvironmentHazardIdleState IdleStateData;

    public IdleState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName, D_EnvironmentHazardIdleState idleStateData)
        : base(environmentHazardEntity, animationBoolName) => IdleStateData = idleStateData;
}
