using UnityEngine;

[CreateAssetMenu(fileName = "_ActiveOnContact", menuName = "Data/Environment Hazard Active on Contact")]
public class D_EnvironmentHazardActiveOnContactStats : D_EnvironmentHazardStats
{
    public float timeBeforeActivation = 1.0f;
}
