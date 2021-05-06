public class Javelin : Projectile
{
    protected override void ApplyProjectileEffect() => DamageHit.gameObject.transform.parent.GetComponentInParent<IDamagable>()?.Damage();
}
