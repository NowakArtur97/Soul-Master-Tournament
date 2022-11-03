using UnityEngine;

public class IdleForTimeState : IdleState
{
    public IdleForTimeState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData) : base(environmentHazardEntity, animationBoolName, idleStateData) { }

    public override void LogicUpdate()
    {
        if (Time.time >= StateStartTime + IdleStateData.idleTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.ActiveState);
        }
    }
}