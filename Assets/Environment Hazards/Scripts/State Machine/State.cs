public class State
{
    protected readonly EnvironmentHazardEntity EnvironmentHazardEntity;

    protected bool IsExitingState { get; private set; }
    protected bool IsAnimationFinished { get; private set; }
    protected float StateStartTime { get; private set; }

    private string _animationBoolName;

    public State(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName)
    {
        EnvironmentHazardEntity = environmentHazardEntity;
        _animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
