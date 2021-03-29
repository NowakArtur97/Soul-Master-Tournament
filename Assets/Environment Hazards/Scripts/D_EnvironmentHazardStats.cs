using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardData", menuName = "Data/EnvironmentHazard/Data")]
public class D_EnvironmentHazardStats : ScriptableObject
{
    public float idleTime;
    public int damage;
    public LayerMask[] whatIsDamagable;
}
