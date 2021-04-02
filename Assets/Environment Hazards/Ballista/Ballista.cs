using UnityEngine;

public class Ballista : EnvironmentHazard
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStartingPosition;

    protected override void UseEnvironmentHazard()
    {
        Instantiate(projectile, projectileStartingPosition.position, Quaternion.identity);
        IsActive = false;
    }
}
