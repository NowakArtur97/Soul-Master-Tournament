using UnityEngine;

public class EvilEyeSoul : Soul
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

            if (CheckIfTouchingWall(range, explosionDirection))
            {
                return;
            }

            GameObject explosion = Instantiate(SoulAbility, explosionPosition, Quaternion.identity);

            string animationBoolName = range != explosionRange ? "middle" : "end";

            explosion.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);
        }
    }
}
