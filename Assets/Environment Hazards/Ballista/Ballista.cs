using UnityEngine;

public class Ballista : EnvironmentHazardActiveAfterTime
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStartingPosition;

    private GameObject _projectile;

    protected override void UseEnvironmentHazard()
    {
        _projectile = Instantiate(projectile, projectileStartingPosition.position, transform.rotation);
        CurrentStatus = Status.FINISHED;
    }
}
