using UnityEngine;

public class LaunchProjectileState : ActiveState
{
    private ProjectileLauncherEntity _projectileLauncherEntity;
    private D_EnvironmentHazardLaunchProjectileState _stateData;
    private Transform _projectileStartingPosition;

    public LaunchProjectileState(ProjectileLauncherEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardLaunchProjectileState stateData, Transform projectileStartingPosition)
        : base(environmentHazardEntity, animationBoolName)
    {
        _projectileLauncherEntity = environmentHazardEntity;
        _stateData = stateData;
        _projectileStartingPosition = projectileStartingPosition;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState && IsAnimationFinished)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        GameObject.Instantiate(_stateData.projectile, _projectileStartingPosition.position,
            EnvironmentHazardEntity.CoreContainer.gameObject.transform.rotation);
    }
}
