public class StartBlockingState : State
{
    private MovingWallEntity _environmentHazardEntity;

    public StartBlockingState(MovingWallEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName) => _environmentHazardEntity = environmentHazardEntity;

    public override void Enter()
    {
        base.Enter();

        _environmentHazardEntity.MyBoxCollider2D.enabled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_environmentHazardEntity.ActiveState);
        }
    }
}
