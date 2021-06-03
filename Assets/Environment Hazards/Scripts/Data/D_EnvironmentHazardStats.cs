using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardData", menuName = "Data/Environment Hazard Stats")]
public class D_EnvironmentHazardStats : ScriptableObject
{
    public LayerMask[] whatIsInteractable;
}
