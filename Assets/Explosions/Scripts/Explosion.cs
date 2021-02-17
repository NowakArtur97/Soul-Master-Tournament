using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    protected D_ExplosionStats ExplosionStats;

    protected bool HasExploded;

    private GameObject _aliveGameObject;
    private ExplosionAnimationToComponent _explosionAnimationToComponent;
    private Animator _myAnimator;

    private void Awake()
    {
        _aliveGameObject = transform.Find("Alive").gameObject;
        _myAnimator = _aliveGameObject.GetComponent<Animator>();
        _explosionAnimationToComponent = _aliveGameObject.GetComponent<ExplosionAnimationToComponent>();

        _explosionAnimationToComponent.Explosion = this;

        transform.position += (Vector3)ExplosionStats.positionOffset;
    }

    public virtual void ExplodedTrigger()
    {
        HasExploded = true;
    }
}
