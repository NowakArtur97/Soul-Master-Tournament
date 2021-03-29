using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardData", menuName = "Data/EnvironmentHazard/Data")]
public class D_EnvironmentHazardStats : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 6f;
    public int damage = 1;
    public LayerMask[] whatIsDamagable;
}
