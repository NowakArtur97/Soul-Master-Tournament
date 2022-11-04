public class PermamentDamangeOnContactState : ActiveState
{
    public PermamentDamangeOnContactState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName)
    { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Damage();
    }
}
