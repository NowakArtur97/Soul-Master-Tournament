using UnityEngine;

public class SoulAbility : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField]
    private Vector2 _positionOffset = new Vector2(0, 0);

    protected bool HasFinished;
    protected bool IsActive;
    protected float StartTime { get; private set; }

    protected GameObject AliveGameObject { get; private set; }
    private AbilityAnimationToComponent _abilityAnimationToComponent;

    protected virtual void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _abilityAnimationToComponent = AliveGameObject.GetComponent<AbilityAnimationToComponent>();

        _abilityAnimationToComponent.SoulAbility = this;

        StartTime = Time.time;

        transform.position = transform.position + (Vector3)_positionOffset;
    }

    protected virtual void Update()
    {
        if (HasFinished)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) { }

    public virtual void ActiveTrigger() => IsActive = true;

    public virtual void FinishTrigger() => HasFinished = true;
}
