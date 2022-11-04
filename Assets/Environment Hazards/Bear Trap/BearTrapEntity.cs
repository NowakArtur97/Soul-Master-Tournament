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
        IdleState = new IdleOnContactState(this, "idle", IdleStateData);
        ActiveState = new TrapPlayerOnAnimationTriggerState(this, "active", _trapPlayerStateData);

        base.Awake();
    }
}
