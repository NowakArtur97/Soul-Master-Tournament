using UnityEngine;

public class SoulAbility : MonoBehaviour
{
    [SerializeField]
    protected D_SoulAbilityStats AbilityStats;

    protected bool HasFinished;

    private GameObject _aliveGameObject;
    private AbilityAnimationToComponent _explosionAnimationToComponent;
    private Animator _myAnimator;

    private void Awake()
    {
        _aliveGameObject = transform.Find("Alive").gameObject;
        _myAnimator = _aliveGameObject.GetComponent<Animator>();
        _explosionAnimationToComponent = _aliveGameObject.GetComponent<AbilityAnimationToComponent>();

        _explosionAnimationToComponent.SoulAbility = this;
    }

    protected virtual void Update()
    {
        if (HasFinished)
        {
            Destroy(gameObject);
        }
    }

    public virtual void FinishTrigger()
    {
        HasFinished = true;
    }
}
