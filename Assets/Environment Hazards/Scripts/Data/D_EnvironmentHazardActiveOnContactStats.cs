using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardActiveOnContactData", menuName = "Data/Environment Hazard Active on Contact")]
public class D_EnvironmentHazardActiveOnContactStats : D_EnvironmentHazardStats
{
    public float timeBeforeActivation = 1.0f;
    public bool isActiveOnTrigger = false;
}
