using UnityEngine;

public class Spikes : EnvironmentHazardActiveAfterTime
{
    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;

        if (CheckIfPlayerInMinAgro(out toDamage, EnvironmentHazardData.whatIsInteractable))
        {
            toDamage.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage();
        }
    }
}
