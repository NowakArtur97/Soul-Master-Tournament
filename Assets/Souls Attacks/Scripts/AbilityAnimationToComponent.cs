using UnityEngine;

public class AbilityAnimationToComponent : MonoBehaviour
{
    public SoulAbility SoulAbility;

    private void ActiveTrigger() => SoulAbility.ActiveTrigger();

    private void FinishTrigger() => SoulAbility.FinishTrigger();
}
