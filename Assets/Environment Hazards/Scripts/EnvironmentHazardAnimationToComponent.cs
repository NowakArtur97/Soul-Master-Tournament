using UnityEngine;

public class EnvironmentHazardAnimationToComponent : MonoBehaviour
{
    public EnvironmentHazard EnvironmentHazard;

    private void UseEnvironmentHazardTrigger() => EnvironmentHazard.UseEnvironmentHazardTrigger();

    private void StopUsingEnvironmentHazardTrigger() => EnvironmentHazard.StopUsingEnvironmentHazardTrigger();
}
