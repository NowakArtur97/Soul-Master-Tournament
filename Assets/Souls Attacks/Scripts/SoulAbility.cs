using UnityEngine;

public class SoulAbility : MonoBehaviour
{
    [SerializeField]
    protected D_SoulAbilityStats AbilityStats;

    protected bool HasFinished;
    protected float StartTime;

    private GameObject _aliveGameObject;
    private AbilityAnimationToComponent _explosionAnimationToComponent;
    private Animator _myAnimator;

    private void Awake()
    {
        _aliveGameObject = transform.Find("Alive").gameObject;
        _myAnimator = _aliveGameObject.GetComponent<Animator>();
        _explosionAnimationToComponent = _aliveGameObject.GetComponent<AbilityAnimationToComponent>();

        _explosionAnimationToComponent.SoulAbility = this;

        StartTime = Time.time;
    }

    protected virtual void Update()
    {
        if (HasFinished)
        {
            Destroy(gameObject);
        }
    }

    // TODO: Ice Wall: If only Ice Wall call OnTriggerEnter2D then change ChildToParentTrggier
    public virtual void OnTriggerEnter2D(Collider2D collision) { }

    public virtual void FinishTrigger() => HasFinished = true;
}
