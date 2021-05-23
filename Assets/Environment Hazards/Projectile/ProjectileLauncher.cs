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

    protected override void Awake()
    {
        base.Awake();

        SetOffsetFromWall(_offsetsFromWall);
    }

    protected override void UseEnvironmentHazard()
    {
        _projectile = Instantiate(projectile, projectileStartingPosition.position, AliveGameObject.transform.rotation);
        CurrentStatus = Status.FINISHED;
    }
}

