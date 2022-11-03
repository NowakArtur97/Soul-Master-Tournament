using UnityEngine;

public class LaunchProjectileState : ActiveState
{
    private ProjectileLauncherEntity _projectileLauncherEntity;
    private D_EnvironmentHazardLaunchProjectileState _stateData;

    public LaunchProjectileState(ProjectileLauncherEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardLaunchProjectileState stateData) : base(environmentHazardEntity, animationBoolName)
    {
        _projectileLauncherEntity = environmentHazardEntity;
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Object.Instantiate(_stateData.projectile, _projectileLauncherEntity.ProjectileStartingPosition.position,
            EnvironmentHazardEntity.AliveGameObject.transform.rotation);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }
}
