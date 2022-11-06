using System.Collections.Generic;
using UnityEngine;

public class PermamentDamangeOnTriggerState : ActiveState
{
    private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;

    private List<GameObject> _allInAgro;
    private bool _isActive;

    public PermamentDamangeOnTriggerState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardDealDamageOnContactState dealDamageOnContactStateData)
        : base(environmentHazardEntity, animationBoolName)
    {
        _dealDamageOnContactStateData = dealDamageOnContactStateData;
        _allInAgro = new List<GameObject>();
    }

    public override void Enter()
    {
        base.Enter();

        _isActive = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isActive && _allInAgro.Count > 0)
        {
            DamageAll(_allInAgro);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_isActive)
        {
            _allInAgro = GetAllInMinAgro(_dealDamageOnContactStateData.whatIsDamagable);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isActive = true;
    }
}
