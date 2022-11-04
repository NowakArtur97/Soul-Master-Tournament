using UnityEngine;

public class CrumblingFloorEntity : EnvironmentHazardEntity
{
    [SerializeField] public Sprite[] FloorSprites;

    public PermamentDamangeOnContactState PermamentDamangeOnContactState { get; private set; }

    protected override void Awake()
    {
        IdleState = new IdleOnContactState(this, "idle", IdleStateData);
        ActiveState = new CrumblingFloorActiveState(this, "active");
        PermamentDamangeOnContactState = new PermamentDamangeOnContactState(this, "active");

        base.Awake();
    }
}
