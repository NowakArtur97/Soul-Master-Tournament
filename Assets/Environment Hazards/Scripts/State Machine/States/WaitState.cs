using UnityEngine;

public class WaitState : State
{
    private D_EnvironmentHazardWaitState _stateData;
    private State _nextState;

    public WaitState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName, D_EnvironmentHazardWaitState stateData,
        State nextState) : base(environmentHazardEntity, animationBoolName)
    {
        _stateData = stateData;
        _nextState = nextState;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState && Time.time >= StateStartTime + _stateData.waitTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_nextState);
        }
    }
}
