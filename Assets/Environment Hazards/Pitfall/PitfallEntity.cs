using UnityEngine;

public class PitfallEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;
    public D_EnvironmentHazardDealDamageOnContactState DealDamageOnContactStateData
    {
        get { return _dealDamageOnContactStateData; }
        private set { _dealDamageOnContactStateData = value; }
    }

    public PermamentDamangeOnContactState PermamentDamangeOnContactState { get; private set; }

    protected override void Awake()
    {
        IdleState = new IdleOnContactState(this, "idle", IdleStateData);
        ActiveState = new PermamentDamangeOnContactState(this, "active", _dealDamageOnContactStateData);
        PlayerDetectedState = new PlayerDetectedState(this, "idle", PlayerDetectedStateData);

        base.Awake();
    }
}