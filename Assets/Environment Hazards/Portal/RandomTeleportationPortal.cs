using UnityEngine;

public class RandomTeleportationPortal : Portal
{
    private static RandomTeleportationPortal[] _portals;

    private void Start() => _portals = FindObjectsOfType<RandomTeleportationPortal>();

    protected override void UseEnvironmentHazard()
    {
        if (ToInteract)
        {
            SetConnectedPortal(ChoseRandomPortal());
        }

        base.UseEnvironmentHazard();
    }

    private RandomTeleportationPortal ChoseRandomPortal() => _portals[Random.Range(0, _portals.Length)];
}
