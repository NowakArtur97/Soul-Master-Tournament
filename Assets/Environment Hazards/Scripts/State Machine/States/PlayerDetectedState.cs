using UnityEngine;

public class PlayerDetectedState : State
{
    private D_EnvironmentHazardPlayerDetectedState _playerDetectedStateData;

    public PlayerDetectedState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardPlayerDetectedState playerDetectedStateData)
        : base(environmentHazardEntity, animationBoolName) => _playerDetectedStateData = playerDetectedStateData;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (EnvironmentHazardEntity.ToInteract.Count > 0
            && Time.time >= StateStartTime + _playerDetectedStateData.timeToWaitAfterDetectingPlayer)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.ActiveState);
        }
        else if (EnvironmentHazardEntity.ToInteract.Count == 0)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }
}
