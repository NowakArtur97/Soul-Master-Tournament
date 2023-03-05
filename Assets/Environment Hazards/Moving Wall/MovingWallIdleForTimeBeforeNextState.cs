using UnityEngine;

public class MovingWallIdleForTimeBeforeNextState : IdleState
{
    private MovingWallEntity _movingWallEntity;
    private State _nextState;

    private bool _isBlocked;

    public MovingWallIdleForTimeBeforeNextState(MovingWallEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardIdleState idleStateData, State nextState)
        : base(environmentHazardEntity, animationBoolName, idleStateData)
    {
        _movingWallEntity = environmentHazardEntity;
        _nextState = nextState;
    }

    public override void Enter()
    {
        base.Enter();

        _isBlocked = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState && Time.time >= StateStartTime + IdleTime && !_isBlocked)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_nextState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _isBlocked = Physics2D.BoxCast(_movingWallEntity.gameObject.transform.position,
            _movingWallEntity.BlockedStateData.isBlockingSizeCheck, 0, EnvironmentHazardEntity.gameObject.transform.right,
            _movingWallEntity.BlockedStateData.isBlockingDistanceCheck, _movingWallEntity.BlockedStateData.whatIsBlocking);
    }
}