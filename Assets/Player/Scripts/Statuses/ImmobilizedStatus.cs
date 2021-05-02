public class ImmobilizedStatus : PlayerStatus
{
    public ImmobilizedStatus(float activeTime) : base(activeTime)
    {
    }

    public override void ApplyStatus(PlayerStatusesManager playerStatusesManager)
    {
        base.ApplyStatus(playerStatusesManager);

        playerStatusesManager.LockMovement();
    }

    public override void CancelStatus(PlayerStatusesManager playerStatusesManager)
    {
        playerStatusesManager.UnlockMovement();
    }
}
