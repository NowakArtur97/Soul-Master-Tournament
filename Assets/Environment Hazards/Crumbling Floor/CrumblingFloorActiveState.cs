public class CrumblingFloorActiveState : ActiveState
{
    private readonly string CRUMBLE_SOUND_CLIP = "CrumblingFloor_Active";

    private CrumblingFloorEntity _crumblingFloorEntity;

    private int _currentSpriteIndex;

    public CrumblingFloorActiveState(CrumblingFloorEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName)
    {
        _crumblingFloorEntity = environmentHazardEntity;
        _currentSpriteIndex = 0;
    }

    public override void Enter()
    {
        base.Enter();

        _currentSpriteIndex++;

        if (!WasCrubled())
        {
            EnvironmentHazardEntity.CoreContainer.SpriteRenderer.sprite = _crumblingFloorEntity.FloorSprites[_currentSpriteIndex];
        }

        AudioManager.Instance.Play(CRUMBLE_SOUND_CLIP);
    }

    public override void LogicUpdate()
    {
        if (WasCrubled())
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_crumblingFloorEntity.PermamentDamangeOnContactState);
        }
        else if (EnvironmentHazardEntity.ToInteract.Count > 0)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(_crumblingFloorEntity.PlayerDetectedState);
        }
        else
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }

    private bool WasCrubled() => _currentSpriteIndex >= _crumblingFloorEntity.FloorSprites.Length;
}
