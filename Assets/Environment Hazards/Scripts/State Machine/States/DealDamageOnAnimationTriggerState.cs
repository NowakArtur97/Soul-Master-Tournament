using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnAnimationTriggerState : ActiveState
{
    private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;
    private List<GameObject> _allInAgro;

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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _allInAgro = GetAllInMinAgro(_dealDamageOnContactStateData.whatIsDamagable);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        DamageAll(_allInAgro);
    }
}
