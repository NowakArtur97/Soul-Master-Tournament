using UnityEngine;

public class IdleOnContactState : IdleState
{
    public IdleOnContactState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData) : base(environmentHazardEntity, animationBoolName, idleStateData) { }

    public override void LogicUpdate()
    {
        if (EnvironmentHazardEntity.ToInteract && Time.time >= StateStartTime + IdleStateData.idleTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.ActiveState);
        }
        if (EnvironmentHazardEntity.ToInteract == null)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }
}