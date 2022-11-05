using UnityEngine;

public class TrapPlayerOnAnimationTriggerState : ActiveState
{
    private D_EnvironmentHazardTrapPlayerState _trapPlayerStateData;

    public TrapPlayerOnAnimationTriggerState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName, D_EnvironmentHazardTrapPlayerState trapPlayerStateData) : base(environmentHazardEntity, animationBoolName)
        => _trapPlayerStateData = trapPlayerStateData;

    public override void Enter()
    {
        base.Enter();

        GameObject firstTrapped = EnvironmentHazardEntity.ToInteract[0];
        Player player = firstTrapped?.GetComponentInParent<Player>();

        if (player != null)
        {
            PlayerStatus immobilizedStatus = new ImmobilizedStatus(_trapPlayerStateData.immobilityTime);
            player.AddStatus(immobilizedStatus);
            firstTrapped.transform.position = EnvironmentHazardEntity.CoreContainer.gameObject.transform.position
                + (Vector3)_trapPlayerStateData.afterBeingTrappedOffset;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (EnvironmentHazardEntity.ToInteract.Count > 0)
            {
                EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.PlayerDetectedState);
            }
            else
            {
                EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
            }
        }
    }
}
