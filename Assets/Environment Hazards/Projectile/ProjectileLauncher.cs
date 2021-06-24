using UnityEngine;

public class ProjectileLauncher : EnvironmentHazardActiveAfterTime
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStartingPosition;
    [SerializeField]
    private Vector2[] _offsetsFromWall;

    private GameObject _projectile;
    private bool _canShot;

    protected override void Awake()
    {
        base.Awake();

        SetOffsetFromWall(_offsetsFromWall);

        _canShot = true;
    }

    protected override void UseEnvironmentHazard()
    {
        if (_canShot)
        {
            _canShot = false;
            _projectile = Instantiate(projectile, projectileStartingPosition.position, AliveGameObject.transform.rotation);
        }
    }

    protected override void TriggerEnvironmentHazard() => SetIsAnimationActive(true);

    protected override void FinishUsingEnvironmentHazard()
    {
        _canShot = true;

        base.FinishUsingEnvironmentHazard();
    }
}

