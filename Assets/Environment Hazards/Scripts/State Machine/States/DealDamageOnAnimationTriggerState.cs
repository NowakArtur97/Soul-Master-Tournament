public class DealDamageOnAnimationTriggerState : ActiveState
{
    private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;

    public DealDamageOnAnimationTriggerState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardDealDamageOnContactState dealDamageOnContactStateData)
       : base(environmentHazardEntity, animationBoolName) => _dealDamageOnContactStateData = dealDamageOnContactStateData;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        DamageAll(GetAllInMinAgro(_dealDamageOnContactStateData.whatIsDamagable));
    }
}
