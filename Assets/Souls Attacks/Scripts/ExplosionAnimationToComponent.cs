using UnityEngine;

public class ExplosionAnimationToComponent : MonoBehaviour
{
    public SoulAbility Explosion;

    private void ExplodedTrigger()
    {
        Explosion.ExplodedTrigger();
    }
}
