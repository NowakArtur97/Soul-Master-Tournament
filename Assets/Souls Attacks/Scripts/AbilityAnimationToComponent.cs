using UnityEngine;

public class AbilityAnimationToComponent : MonoBehaviour
{
    public SoulAbility Explosion;

    private void FinishTrigger()
    {
        Explosion.FinishTrigger();
    }
}
