public class RandomPortalEntity : PortalEntity
{
    public static RandomPortalEntity[] RandomPortalsOnLevel;

    protected override void Awake()
    {
        base.Awake();

        ActiveState = new RandomTeleportState(this, "active", TeleportStateData);
    }

    protected override void Start()
    {
        base.Start();

        RandomPortalsOnLevel = FindObjectsOfType<RandomPortalEntity>();
    }
}
