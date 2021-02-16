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
            Explode();
        }
        else if (IsExploding)
        {
            MyAnimator.SetBool("explode", true);
        }
    }

    protected override void Explode()
    {
        base.Explode();

        for (int index = 0; index < ExplosionDirections.Length; index++)
        {
            SpawnExplosion(ExplosionDirections[index]);
        }
    }

    protected override void SpawnExplosion(Vector2 explosionDirection)
    {
        base.SpawnExplosion(explosionDirection);

        int explosionRange = SoulStats.explosionRange;

        Vector2 explosionPosition;

        for (int range = 1; range <= explosionRange; range++)
        {
            explosionPosition = (Vector2)transform.position + range * explosionDirection;

            GameObject explosion = Instantiate(Explosion, explosionPosition, Quaternion.identity);

            string animationBoolName = range != explosionRange ? "middle" : "end";

            explosion.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);

            Debug.Log(explosionPosition);
        }
    }
}
