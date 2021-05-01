public class ReversedControlsStatus : PlayerStatus
{
    public ReversedControlsStatus(float activeTime) : base(activeTime)
    {
    }

    public override void ApplyStatus(PlayerStatusesManager playerStatusesManager)
    {
        base.ApplyStatus(playerStatusesManager);

        playerStatusesManager.ReverseControls();
    }

    public override void CancelStatus(PlayerStatusesManager playerStatusesManager)
    {
        playerStatusesManager.CancelReversingControls();
    }
}
