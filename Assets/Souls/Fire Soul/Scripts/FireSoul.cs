using UnityEngine;

public class FireSoul : Soul
{
    protected override void Update()
    {
        base.Update();

        if (IsExploding)
        {
            Explode();
            Debug.Log("EXPLODE");
        }
    }

    protected override void Explode()
    {
        base.Explode();

        //for (int index = 0; index < ExplosionDirections.Length; index++)
        //{
        //    SpawnExplosion(ExplosionDirections[index]);
        //}
    }

    protected override void SpawnExplosion(Vector2 explosionPosition)
    {
        base.SpawnExplosion(explosionPosition);

        Vector2 startPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        int explosionRange = SoulStats.explosionRange;

        for (int i = 0; i <= explosionRange; i++)
        {
            //Instantiate(startPosition, );
        }
    }
}
