using UnityEngine;

public class FireSoul : Soul
{
    private int _index;

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

    protected override void Explode()
    {
        base.Explode();

        for (_index = 0; _index < ExplosionDirections.Length; _index++)
        {
            SpawnExplosion(ExplosionDirections[_index]);
        }
    }

    protected override void SpawnExplosion(Vector2 explosionDirection)
    {
        base.SpawnExplosion(explosionDirection);

        int explosionRange = SoulStats.explosionRange;

        Vector2 explosionPosition;
        Quaternion rotation;

        for (int range = 1; range <= explosionRange; range++)
        {
            explosionPosition = (Vector2)transform.position + range * explosionDirection;

            rotation = transform.rotation;
            rotation.z -= 90 * _index;

            GameObject explosion = Instantiate(Explosion, explosionPosition, rotation);

            string animationBoolName = range != explosionRange ? "middle" : "end";

            explosion.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);

            Debug.Log(rotation);
        }
    }
}
