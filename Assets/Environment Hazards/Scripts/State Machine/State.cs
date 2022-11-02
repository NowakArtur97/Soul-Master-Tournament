using UnityEngine;

public abstract class State
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
        IsExitingState = false;
        IsAnimationFinished = false;
        StateStartTime = Time.time;

        if (EnvironmentHazardEntity.CoreContainer.AnimationToStateMachine != null)
        {
            EnvironmentHazardEntity.CoreContainer.AnimationToStateMachine.CurrentState = this;
        }

        EnvironmentHazardEntity.CoreContainer.Animation?.SetBoolVariable(_animationBoolName, true);
    }

    public abstract void LogicUpdate();

    public virtual void Exit()
    {
        EnvironmentHazardEntity.CoreContainer.Animation?.SetBoolVariable(_animationBoolName, false);

        IsExitingState = true;
    }

    public virtual void AnimationTrigger() { }

    public void AnimationFinishedTrigger() => IsAnimationFinished = true;
}
