using UnityEngine;

public class SoulAbility : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField]
    protected D_SoulAbilityStats AbilityStats;

    protected bool HasFinished;
    protected bool IsActive;
    protected float StartTime { get; private set; }

    private GameObject _aliveGameObject;
    private AbilityAnimationToComponent _explosionAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    private void Awake()
    {
        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        MyAnimator = _aliveGameObject.GetComponent<Animator>();
        _explosionAnimationToComponent = _aliveGameObject.GetComponent<AbilityAnimationToComponent>();

        _explosionAnimationToComponent.SoulAbility = this;

        StartTime = Time.time;

        transform.position = transform.position + (Vector3)AbilityStats.positionOffset;
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

    public virtual void ActiveTrigger() => IsActive = true;

    public virtual void FinishTrigger() => HasFinished = true;
}
