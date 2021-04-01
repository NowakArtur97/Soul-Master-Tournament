using UnityEngine;

public class EnvironmentHazardAnimationToComponent : MonoBehaviour
{
    public EnvironmentHazard EnvironmentHazard;

    private void StartUsingEnvironmentHazardTrigger() => EnvironmentHazard.StartUsingEnvironmentHazardTrigger();

    private void StopUsingEnvironmentHazardTrigger() => EnvironmentHazard.StopUsingEnvironmentHazardTrigger();
}
