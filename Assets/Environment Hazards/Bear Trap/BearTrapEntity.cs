using UnityEngine;

public class BearTrapEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardTrapPlayerState _trapPlayerStateData;
    public D_EnvironmentHazardTrapPlayerState TrapPlayerStateData
    {
        get { return _trapPlayerStateData; }
        private set { _trapPlayerStateData = value; }
    }

    protected override void Awake()
    {
        IdleState = new IdleOnContactWithDetectingPlayerState(this, "idle", IdleStateData);
        ActiveState = new TrapPlayerOnAnimationTriggerState(this, "active", _trapPlayerStateData);
        PlayerDetectedState = new PlayerDetectedState(this, "idle", PlayerDetectedStateData);

        base.Awake();
    }
}
