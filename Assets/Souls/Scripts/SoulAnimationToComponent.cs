using UnityEngine;

public class SoulAnimationToComponent : MonoBehaviour
{
    public Soul Soul;

    private void StartUsingAbilityTrigger()
    {
        Soul.StartUsingAbilityTrigger();
    }

    private void FinishUsingAbilityTrigger()
    {
        Soul.FinishUsingAbilityTrigger();
    }
}
