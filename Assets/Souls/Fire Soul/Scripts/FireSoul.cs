using UnityEngine;

public class FireSoul : Soul
{
    protected override void Update()
    {
        base.Update();

        if (ShouldStartSpawningExplosions)
        {
            Explode();
        }

        if (HasExploded)
        {
            Destroy(gameObject);
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

    protected override void SpawnExplosion(Vector2 explosionPosition)
    {
        base.SpawnExplosion(explosionPosition);

        Vector2 startPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        int explosionRange = SoulStats.explosionRange;

        for (int i = 1; i <= explosionRange; i++)
        {
            startPosition += ExplosionDirections[i - 1];
            GameObject explosion = Instantiate(Explosion, startPosition, Quaternion.identity);

            string animationBoolName = i != explosionRange ? "middle" : "end";

            explosion.GetComponent<Animator>().SetBool(animationBoolName, true);
        }
    }
}
