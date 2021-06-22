using UnityEngine;

public class SoulAnimationToComponent : MonoBehaviour
{
    public Soul Soul;

    private void SummonedTrigger() => Soul.SummonedTrigger();

    private void UnsummonedTrigger() => Soul.UnsummonedTrigger();

    private void StartUsingAbilityTrigger() => Soul.StartUsingAbilityTrigger();

    private void FinishUsingAbilityTrigger() => Soul.FinishUsingAbilityTrigger();

    private void PlaySummonSoundTrigger() => Soul.PlaySummonSoundTrigger();

    private void PauseSummonSoundTrigger() => Soul.PauseSummonSoundTrigger();

    private void PlayUnsummonSoundTrigger() => Soul.PlayUnsummonSoundTrigger();

    private void PauseUnsummonSoundTrigger() => Soul.PauseUnsummonSoundTrigger();

    private void PlayAbilitySoundTrigger() => Soul.PlayAbilitySoundTrigger();

    private void PauseAbilitySoundTrigger() => Soul.PauseAbilitySoundTrigger();
}
