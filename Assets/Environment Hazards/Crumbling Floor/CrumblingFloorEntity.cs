using UnityEngine;

public class CrumblingFloorEntity : EnvironmentHazardEntity
{
    [SerializeField] public Sprite[] FloorSprites;

    protected override void Awake()
    {
        base.Awake();

        ActiveState = new CrumblingFloorActiveState(this, "active");
    }
}
