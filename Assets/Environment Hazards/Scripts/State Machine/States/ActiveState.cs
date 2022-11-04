using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ActiveState : State
{
    public ActiveState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName) { }

    protected void Damage() => EnvironmentHazardEntity.ToInteract
        .ForEach(damagable => damagable.GetComponentInParent<IDamagable>()?.Damage());

    protected void DamageAll(List<GameObject> toInteract) =>
        toInteract.ForEach(damagable => damagable.GetComponentInParent<IDamagable>()?.Damage());

    protected List<GameObject> GetAllInMinAgro(LayerMask[] whatIsDamagable)
    {
        List<GameObject> toInteract = new List<GameObject>();

        foreach (LayerMask damagableLayerMask in whatIsDamagable)
        {
            Collider2D[] collisions = Physics2D.OverlapBoxAll(EnvironmentHazardEntity.CoreContainer.gameObject.transform.position,
                Vector2.one, 0f, damagableLayerMask);

            if (collisions.Length > 0)
            {
                collisions.ToList()
                    .ForEach(collision => toInteract.Add(collision.gameObject));
            }
        }

        return toInteract;
    }
}
