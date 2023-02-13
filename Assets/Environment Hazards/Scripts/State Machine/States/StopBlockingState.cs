public class StopBlockingState : State
{
    private MovingWallEntity _environmentHazardEntity;

    public StopBlockingState(MovingWallEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName) => _environmentHazardEntity = environmentHazardEntity;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_environmentHazardEntity.IdleState);
        }
    }

    public override void Exit()
    {
        base.Enter();

        _environmentHazardEntity.MyBoxCollider2D.enabled = true;
    }
}

