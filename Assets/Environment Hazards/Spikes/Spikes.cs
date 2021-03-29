using UnityEngine;

public class Spikes : EnvironmentHazard
{
    private GameObject _toDamage;

    protected override void ActivateEnvironmentHazard()
    {
        base.ActivateEnvironmentHazard();

        if (CheckIfPlayerInMinAgro())
        {
            _toDamage.transform.parent.SendMessage("Damage", AttackDetails);
        }
    }

    public virtual bool CheckIfPlayerInMinAgro()
    {
        foreach (LayerMask damagableLayerMask in EnvironmentHazardData.whatIsDamagable)
        {
            Collider2D collision = Physics2D.OverlapBox(AliveGameObject.transform.position, Vector2.one, 0f, damagableLayerMask);
            if (collision)
            {
                _toDamage = collision.gameObject;
                return true;
            }
        }

        return false;
    }
}
