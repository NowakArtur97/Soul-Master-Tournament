public class BallistaEntity : ProjectileLauncherEntity
{
    protected override void Awake()
    {
        base.Awake();

        IdleState = new IdleForTimeState(this, "idle", IdleStateData);
    }
}
