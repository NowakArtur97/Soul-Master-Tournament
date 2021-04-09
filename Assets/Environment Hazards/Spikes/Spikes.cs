using UnityEngine;

public class Spikes : EnvironmentHazardDamagingOnContact
{
    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;

        if (CheckIfPlayerInMinAgro(out toDamage))
        {
            toDamage.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage(AttackDetails);
        }
    }
}
