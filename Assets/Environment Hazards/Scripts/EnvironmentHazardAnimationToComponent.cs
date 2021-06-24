using UnityEngine;

public class EnvironmentHazardAnimationToComponent : MonoBehaviour
{
    public EnvironmentHazard EnvironmentHazard;

    private void StartUsingEnvironmentHazardTrigger() => EnvironmentHazard.StartUsingEnvironmentHazardTrigger();

    private void StopUsingEnvironmentHazardTrigger() => EnvironmentHazard.StopUsingEnvironmentHazardTrigger();

    private void PlayActiveSoundTrigger() => EnvironmentHazard.PlayActiveSoundTrigger();

    private void PauseActiveSoundTrigger() => EnvironmentHazard.PauseActiveSoundTrigger();

    private void PlayActivedSoundTrigger() => EnvironmentHazard.PlayActivedSoundTrigger();
}
