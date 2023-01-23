using System.Linq;
using UnityEngine;

public class RandomTeleportState : TeleportState
{
    private RandomPortalEntity _environmentHazardEntity;

    public RandomTeleportState(RandomPortalEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardTeleportState teleportStateData) : base(environmentHazardEntity, animationBoolName, teleportStateData)
        => _environmentHazardEntity = environmentHazardEntity;

    public override void Enter()
    {
        base.Enter();

        _environmentHazardEntity.SetConnectedPortal(ChoseRandomPortal());
    }

    private RandomPortalEntity ChoseRandomPortal() =>
        RandomPortalEntity.RandomPortalsOnLevel
            .Where(Portal => Portal != _environmentHazardEntity)
            .ToArray()
            [Random.Range(0, RandomPortalEntity.RandomPortalsOnLevel.Length - 1)]; // minus one for this Portal
}
