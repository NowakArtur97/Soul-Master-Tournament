public class ProtectedStatus : PlayerStatus
{
    public ProtectedStatus(float activeTime) : base(activeTime)
    {
    }

    public override void ApplyStatus(PlayerStatusesManager playerStatusesManager)
    {
        base.ApplyStatus(playerStatusesManager);

        playerStatusesManager.ActivateShield();
    }
}
