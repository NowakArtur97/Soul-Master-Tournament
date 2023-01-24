public class PermamentDamangeOnContactState : ActiveState
{
    private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;

    public PermamentDamangeOnContactState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardDealDamageOnContactState dealDamageOnContactStateData)
        : base(environmentHazardEntity, animationBoolName) => _dealDamageOnContactStateData = dealDamageOnContactStateData;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        DamageAll(EnvironmentHazardEntity.ToInteract);
    }
}
