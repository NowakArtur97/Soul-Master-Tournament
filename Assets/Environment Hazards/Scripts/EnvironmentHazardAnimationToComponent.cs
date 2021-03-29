using UnityEngine;

public class EnvironmentHazardAnimationToComponent : MonoBehaviour
{
    public EnvironmentHazard EnvironmentHazard;

    private void ActivatedTrigger() => EnvironmentHazard.ActivatedTrigger();
}
