using UnityEngine;

public class ExplosionAnimationToComponent : MonoBehaviour
{
    public Explosion Explosion;

    private void ExplodedTrigger()
    {
        Explosion.ExplodedTrigger();
    }
}
