using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnContactState : ActiveState
{
    private D_EnvironmentHazardDealDamageOnContactState _dealDamageOnContactStateData;

    public DealDamageOnContactState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName,
        D_EnvironmentHazardDealDamageOnContactState dealDamageOnContactStateData)
       : base(environmentHazardEntity, animationBoolName)
    {
        _dealDamageOnContactStateData = dealDamageOnContactStateData;
    }

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

        DamageAll(GetAllInMinAgro());
    }

    protected List<GameObject> GetAllInMinAgro()
    {
        List<GameObject> toInteract = new List<GameObject>();

        foreach (LayerMask damagableLayerMask in _dealDamageOnContactStateData.whatIsDamagable)
        {
            Collider2D collision = Physics2D.OverlapBox(EnvironmentHazardEntity.CoreContainer.gameObject.transform.position,
                Vector2.one, 0f, damagableLayerMask);

            if (collision)
            {
                toInteract.Add(collision.gameObject);
            }
        }

        return toInteract;
    }
}
