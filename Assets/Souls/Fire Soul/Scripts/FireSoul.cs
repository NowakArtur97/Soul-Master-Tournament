using UnityEngine;

public class FireSoul : Soul
{
    protected override void Update()
    {
        base.Update();

        if (HasExploded)
        {
            Destroy(gameObject);
        }
        else if (ShouldStartSpawningExplosions)
        {
            ShouldStartSpawningExplosions = false;
            Explode();
        }
        else if (IsExploding)
        {
            IsExploding = false;
            MyAnimator.SetBool("explode", true);
        }
    }

    protected override void SpawnExplosion(Vector2 explosionDirection)
    {
        int explosionRange = SoulStats.explosionRange;

        Vector2 explosionPosition;

        for (int range = 1; range <= explosionRange; range++)
        {
            explosionPosition = (Vector2)transform.position + range * explosionDirection;

            GameObject explosion = Instantiate(Explosion, explosionPosition, Quaternion.Euler(0, 0, -90 * ExplosionDirectionIndex));

            string animationBoolName = range != explosionRange ? "middle" : "end";

            explosion.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);
        }
    }
}
