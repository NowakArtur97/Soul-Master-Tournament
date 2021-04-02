using UnityEngine;

public class Spikes : EnvironmentHazard
{
    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;

        if (CheckIfPlayerInMinAgro(out toDamage))
        {
            toDamage.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage(AttackDetails);
        }
    }

    public virtual bool CheckIfPlayerInMinAgro(out GameObject toDamage)
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
