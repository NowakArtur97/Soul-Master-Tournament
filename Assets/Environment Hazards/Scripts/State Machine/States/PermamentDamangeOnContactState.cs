using System.Collections.Generic;
using UnityEngine;

public class PermamentDamangeOnContactState : ActiveState
{
    private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;

    private List<GameObject> _allInAgro;

    public PermamentDamangeOnContactState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardDealDamageOnContactState dealDamageOnContactStateData)
        : base(environmentHazardEntity, animationBoolName)
    {
        _dealDamageOnContactStateData = dealDamageOnContactStateData;
        _allInAgro = new List<GameObject>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_allInAgro.Count > 0)
        {
            DamageAll(_allInAgro);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _allInAgro = GetAllInMinAgro(_dealDamageOnContactStateData.whatIsDamagable);
    }
}