using UnityEngine;

public class Explosion : MonoBehaviour
{
    protected bool HasExploded;

    public virtual void ExplodedTrigger()
    {
        HasExploded = true;
    }
}
