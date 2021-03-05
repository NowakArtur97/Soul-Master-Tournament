using UnityEngine;

public class AbilityAnimationToComponent : MonoBehaviour
{
    public SoulAbility SoulAbility;

    private void FinishTrigger()
    {
        SoulAbility.FinishTrigger();
    }
}
