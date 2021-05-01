public class Javelin : Projectile
{
    protected override void ApplyProjectileEffect() => DamageHit.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage();
}
