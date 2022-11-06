using UnityEngine;

public class SpikesActiveOnlyOnceEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;
    public D_EnvironmentHazardDealDamageOnContactState DealDamageOnContactStateData
    {
        get { return _dealDamageOnContactStateData; }
        private set { _dealDamageOnContactStateData = value; }
    }

    protected override void Awake()
    {
        ActiveState = new PermamentDamangeOnTriggerState(this, "active", _dealDamageOnContactStateData);
        WaitState = new WaitState(this, "wait", WaitStateData, ActiveState);

        base.Awake();
    }
}