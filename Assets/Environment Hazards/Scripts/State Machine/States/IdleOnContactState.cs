public class IdleOnContactState : IdleState
{
    public IdleOnContactState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData) : base(environmentHazardEntity, animationBoolName, idleStateData) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (EnvironmentHazardEntity.ToInteract.Count > 0)
            {
                EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.PlayerDetectedState);
            }
            if (EnvironmentHazardEntity.ToInteract == null)
            {
                EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
            }
        }
    }
}