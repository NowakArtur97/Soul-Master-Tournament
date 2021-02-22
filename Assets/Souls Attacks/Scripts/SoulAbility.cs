using UnityEngine;

public class SoulAbility : MonoBehaviour
{
    [SerializeField]
    protected D_SoulAbilityStats AttackStats;

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

    private void Update()
    {
        if (HasExploded)
        {
            Destroy(gameObject);
        }
    }

    public virtual void ExplodedTrigger()
    {
        HasExploded = true;
    }
}
