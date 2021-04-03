using UnityEngine;

public class Ballista : EnvironmentHazard
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStartingPosition;

    private GameObject _projectile;

    protected override void UseEnvironmentHazard()
    {
        _projectile = Instantiate(projectile, projectileStartingPosition.position, Quaternion.identity) as GameObject;
        _projectile.GetComponent<Projectile>().SetDirection(transform.rotation.eulerAngles);
        IsActive = false;
    }
}
