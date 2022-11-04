using UnityEngine;

public class SpikesEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;
    public D_EnvironmentHazardDealDamageOnContactState DealDamageOnContactStateData
    {
        get { return _dealDamageOnContactStateData; }
        private set { _dealDamageOnContactStateData = value; }
    }

    protected override void Awake()
    {
        IdleState = new IdleForTimeState(this, "idle", IdleStateData);
        ActiveState = new DealDamageOnAnimationTriggerState(this, "active", _dealDamageOnContactStateData);

        base.Awake();
    }
}
