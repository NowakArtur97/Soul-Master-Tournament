using UnityEngine;

public abstract class EnvironmentHazardDamagingOnContact : EnvironmentHazard
{
    protected bool CheckIfPlayerInMinAgro(out GameObject toDamage)
    {
        foreach (LayerMask damagableLayerMask in EnvironmentHazardData.whatIsDamagable)
        {
            Collider2D collision = Physics2D.OverlapBox(AliveGameObject.transform.position, Vector2.one, 0f, damagableLayerMask);
            if (collision)
            {
                toDamage = collision.gameObject;
                return true;
            }
        }

        toDamage = null;
        return false;
    }
}
