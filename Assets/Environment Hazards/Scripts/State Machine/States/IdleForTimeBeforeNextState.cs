using UnityEngine;

public class IdleForTimeBeforeNextState : IdleState
{
    private State _nextState;

    public IdleForTimeBeforeNextState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData, State nextState)
        : base(environmentHazardEntity, animationBoolName, idleStateData) => _nextState = nextState;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState && Time.time >= StateStartTime + IdleTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_nextState);
        }
    }
}