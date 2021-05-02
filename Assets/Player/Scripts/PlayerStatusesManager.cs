using System.Collections.Generic;

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
        status.ApplyStatus(this);
        _statuses.Add(status);
    }

    public void RemoveStatus(PlayerStatus status)
    {
        status.CancelStatus(this);
        _statuses.Remove(status);
    }

    public void CheckStatuses()
    {
        List<PlayerStatus> toRemove = new List<PlayerStatus>();
        _statuses.ForEach(status =>
        {
            if (!status.ChecIfActive())
            {
                status.CancelStatus(this);
                toRemove.Add(status);
            }
        });

        _statuses.RemoveAll(status => toRemove.Contains(status));
    }

    public bool HasAnyStatusActive() => _statuses.Count > 0;

    public void LockMovement() => CanMove = false;
    public void UnlockMovement() => CanMove = true;

    public void ReverseControls() => HasReversedControls = true;
    public void CancelReversingControls() => HasReversedControls = false;

    public void ActivateShield() => HasShield = true;
    public void DectivateShield() => HasShield = false;
}
