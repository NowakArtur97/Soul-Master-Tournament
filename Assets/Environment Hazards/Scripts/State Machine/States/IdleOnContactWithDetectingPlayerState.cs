using UnityEngine;

public class IdleOnContactWithDetectingPlayerState : IdleState
{
    public IdleOnContactWithDetectingPlayerState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData) : base(environmentHazardEntity, animationBoolName, idleStateData) { }

    public override void LogicUpdate()
    {
        if (EnvironmentHazardEntity.ToInteract.Count > 0 && Time.time >= StateStartTime + IdleTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.PlayerDetectedState);
        }
        if (EnvironmentHazardEntity.ToInteract == null)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }
}
