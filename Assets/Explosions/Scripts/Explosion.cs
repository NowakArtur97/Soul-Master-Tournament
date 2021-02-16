using UnityEngine;

public class Explosion : MonoBehaviour
{
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
    }

    public virtual void ExplodedTrigger()
    {
        HasExploded = true;
    }
}
