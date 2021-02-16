using UnityEngine;

public class ExplosionAnimationToComponent : MonoBehaviour
{
    public Explosion explosion;

    private void ExplodedTrigger()
    {
        explosion.ExplodedTrigger();
    }
}
