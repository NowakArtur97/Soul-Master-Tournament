using System.Collections.Generic;

public abstract class ActiveState : State
{
    public ActiveState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName) { }

    protected void Damage() => EnvironmentHazardEntity.ToInteract?.GetComponentInParent<IDamagable>()?.Damage();

    protected void DamageAll(List<UnityEngine.GameObject> toInteract) =>
        toInteract.ForEach(damagable => damagable.GetComponentInParent<IDamagable>()?.Damage());
}
