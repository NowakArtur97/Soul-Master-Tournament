using UnityEngine;

public class Ballista : EnvironmentHazardOld
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStartingPosition;

    private GameObject _projectile;

    protected override void UseEnvironmentHazard()
    {
        _projectile = Instantiate(projectile, projectileStartingPosition.position, transform.rotation);
        IsActive = false;
    }
}
