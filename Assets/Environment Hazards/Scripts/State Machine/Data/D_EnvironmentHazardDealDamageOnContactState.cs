using UnityEngine;

[CreateAssetMenu(fileName = "_DealDamageOnContactState", menuName = "Data/Environment Hazard Deal Damage On Contact State")]
public class D_EnvironmentHazardDealDamageOnContactState : ScriptableObject
{
    public LayerMask[] whatIsDamagable;
}
