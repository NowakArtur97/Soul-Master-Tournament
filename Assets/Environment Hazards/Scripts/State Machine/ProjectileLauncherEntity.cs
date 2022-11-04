using UnityEngine;

public class ProjectileLauncherEntity : EnvironmentHazardEntity
{
    private const string PROJECTILE_STARTING_POSITION = "Projectile Starting Position";

    [SerializeField] private D_EnvironmentHazardLaunchProjectileState _launchProjectileStateData;
    public D_EnvironmentHazardLaunchProjectileState LaunchProjectileStateData
    {
        get { return _launchProjectileStateData; }
        private set { _launchProjectileStateData = value; }
    }

    protected override void Awake()
    {
        Transform projectileStartingPosition = FindProjectileStartingPosition();

        IdleState = new IdleForTimeState(this, "idle", IdleStateData);
        ActiveState = new LaunchProjectileState(this, "active", _launchProjectileStateData, projectileStartingPosition);

        base.Awake();
    }

    private Transform FindProjectileStartingPosition()
    {
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();

        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform.gameObject.name == PROJECTILE_STARTING_POSITION)
            {
                return childTransform.gameObject.transform;
            }
        }

        return null;
    }
}
