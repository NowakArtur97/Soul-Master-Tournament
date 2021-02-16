using UnityEngine;

public class SoulAnimationToComponent : MonoBehaviour
{
    public Soul soul;

    private void ExplodedTrigger()
    {
        soul.ExplodedTrigger();
    }

    private void StartSpawningExplosionsTrigger()
    {
        soul.StartSpawningExplosionsTrigger();
    }
}
