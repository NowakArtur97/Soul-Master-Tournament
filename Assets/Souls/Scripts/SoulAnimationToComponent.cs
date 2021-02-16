using UnityEngine;

public class SoulAnimationToComponent : MonoBehaviour
{
    public Soul Soul;

    private void ExplodedTrigger()
    {
        Soul.ExplodedTrigger();
    }

    private void StartSpawningExplosionsTrigger()
    {
        Soul.StartSpawningExplosionsTrigger();
    }
}
