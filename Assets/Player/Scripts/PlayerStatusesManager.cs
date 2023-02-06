using System.Collections.Generic;
using System.Linq;

public class PlayerStatusesManager
{
    private List<PlayerStatus> _statuses;

    public bool CanMove { get; private set; }
    public bool HasReversedControls { get; private set; }
    public bool HasShield { get; private set; }

    public PlayerStatusesManager()
    {
        _statuses = new List<PlayerStatus>();
        CanMove = true;
        HasReversedControls = false;
        HasShield = false;
    }

    public void AddStatus(PlayerStatus status)
    {
        _statuses.Add(status);
        status.ApplyStatus(this);
    }

    public void RemoveStatus(PlayerStatus status)
    {
        _statuses.Remove(status);
        status.CancelStatus(this);
    }

    public void RemoveAllStatuses()
    {
        _statuses.Clear();
        UnlockMovement();
        CancelReversingControls();
        DectivateShield();
    }

    public void CheckStatuses() => _statuses
        .Where(status => !status.ChecIfActive())
        .ToList()
        .ForEach(status => RemoveStatus(status));

    public bool HasAnyStatusActive() => _statuses.Count > 0;

    public void LockMovement() => CanMove = false;
    public void UnlockMovement() => CanMove = true;

    public void ReverseControls() => HasReversedControls = true;
    public void CancelReversingControls()
    {
        if (_statuses.OfType<ReversedControlsStatus>().Count() == 0)
        {
            HasReversedControls = false;
        }
    }

    public void ActivateShield() => HasShield = true;
    public void DectivateShield() => HasShield = false;
}
