using UnityEngine;

public class TeleportState : ActiveState
{
    private PortalEntity _environmentHazardEntity;
    private D_EnvironmentHazardTeleportState _teleportStateData;

    public TeleportState(PortalEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardTeleportState teleportStateData) : base(environmentHazardEntity, animationBoolName)
    {
        _environmentHazardEntity = environmentHazardEntity;
        _teleportStateData = teleportStateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StateStartTime + _teleportStateData.teleportWaitTime)
        {
            if (EnvironmentHazardEntity.ToInteract.Count > 0)
            {
                Vector2 newPosition = (Vector2)_environmentHazardEntity.ConnectedPortal.transform.position
                    + _teleportStateData.teleportationOffset;
                EnvironmentHazardEntity.ToInteract.ForEach(toInteract => toInteract.transform.position = newPosition);
            }

            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }

        if (EnvironmentHazardEntity.ToInteract.Count == 0)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        EnvironmentHazardEntity.CoreContainer.Sounds.PauseActiveSound();
    }
}
