using UnityEngine;

public class IdleState : State
{
    private D_EnvironmentHazardIdleState _idleStateData;

    public IdleState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName, D_EnvironmentHazardIdleState idleStateData)
        : base(environmentHazardEntity, animationBoolName)
    {
        _idleStateData = idleStateData;
    }

    public override void LogicUpdate()
    {
        if (EnvironmentHazardEntity.ToInteract && Time.time >= StateStartTime + _idleStateData.idleTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.ActiveState);
        }
        if (EnvironmentHazardEntity.ToInteract == null)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }
}
