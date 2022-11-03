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

    public Transform ProjectileStartingPosition { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        ProjectileStartingPosition = GameObject.Find(PROJECTILE_STARTING_POSITION).gameObject.transform;

        ActiveState = new LaunchProjectileState(this, "active", _launchProjectileStateData);
    }
}
