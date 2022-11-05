using UnityEngine;

public class CrumblingFloorEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;
    public D_EnvironmentHazardDealDamageOnContactState DealDamageOnContactStateData
    {
        get { return _dealDamageOnContactStateData; }
        private set { _dealDamageOnContactStateData = value; }
    }
    [SerializeField] public Sprite[] FloorSprites;

    public PermamentDamangeOnContactState PermamentDamangeOnContactState { get; private set; }

    protected override void Awake()
    {
        IdleState = new IdleOnContactWithDetectingPlayerState(this, "idle", IdleStateData);
        ActiveState = new CrumblingFloorActiveState(this, "active");
        PermamentDamangeOnContactState = new PermamentDamangeOnContactState(this, "active", _dealDamageOnContactStateData);
        PlayerDetectedState = new PlayerDetectedState(this, "idle", PlayerDetectedStateData);

        base.Awake();
    }
}
