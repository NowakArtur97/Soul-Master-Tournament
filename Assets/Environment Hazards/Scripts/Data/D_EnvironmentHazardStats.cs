using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardData", menuName = "Data/EnvironmentHazard")]
public class D_EnvironmentHazardStats : ScriptableObject
{
    public LayerMask[] whatIsInteractable;
}
