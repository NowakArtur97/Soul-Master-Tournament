using UnityEngine;

public class CrumblingFloorEntity : EnvironmentHazardEntity
{
    [SerializeField] public Sprite[] FloorSprites;

    protected override void Awake()
    {
        base.Awake();

        IdleState = new IdleOnContactState(this, "idle", IdleStateData);
        ActiveState = new CrumblingFloorActiveState(this, "active");
    }
}
