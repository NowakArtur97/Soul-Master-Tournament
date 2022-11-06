using UnityEngine;

public class IdleForTimeState : IdleState
{
    public IdleForTimeState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData) : base(environmentHazardEntity, animationBoolName, idleStateData) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState && Time.time >= StateStartTime + IdleTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.ActiveState);
        }
    }
}
