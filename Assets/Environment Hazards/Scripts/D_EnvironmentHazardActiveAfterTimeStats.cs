using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardActiveAfterTimeData", menuName = "Data/EnvironmentHazard After Time")]
public class D_EnvironmentHazardActiveAfterTimeStats : D_EnvironmentHazardStats
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 6f;

    public bool isActiveOnTrigger = true;
}
