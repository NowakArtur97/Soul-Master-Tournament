using UnityEngine;

public abstract class State
{
    private readonly string ON_WALL_ANIMATION_NAME_MODIFIER = "-onWall";

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

        EnvironmentHazardEntity.CoreContainer.Animation?.SetBoolVariable(GetAnimationBoolName(), true);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit()
    {
        EnvironmentHazardEntity.CoreContainer.Animation?.SetBoolVariable(GetAnimationBoolName(), false);

        IsExitingState = true;
    }

    public virtual void AnimationTrigger() { }

    public void AnimationFinishedTrigger() => IsAnimationFinished = true;

    public void PlayActiveSoundTrigger() => EnvironmentHazardEntity.CoreContainer.Sounds.PlayActiveSound();

    public void PlayActivedSoundTrigger() => EnvironmentHazardEntity.CoreContainer.Sounds.PlayActivedSound();

    private string GetAnimationBoolName() => EnvironmentHazardEntity.IsOnWall && EnvironmentHazardEntity.CanBeOnWall
            ? _animationBoolName + ON_WALL_ANIMATION_NAME_MODIFIER
            : _animationBoolName;
}
