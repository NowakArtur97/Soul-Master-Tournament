using UnityEngine;

public abstract class PlayerStatus
{
    private float _startTime;
    private float _activateTime;

    public PlayerStatus(float activeTime)
    {
        _activateTime = activeTime;
    }

    public virtual void ApplyStatus(PlayerStatusesManager playerStatusesManager) => _startTime = Time.time;

    public abstract void CancelStatus(PlayerStatusesManager playerStatusesManager);

    public bool ChecIfActive() => Time.time < _startTime + _activateTime;
}
