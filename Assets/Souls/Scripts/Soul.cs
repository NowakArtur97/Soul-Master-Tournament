using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    [SerializeField]
    protected D_SoulStats SoulStats;
    [SerializeField]
    protected SoulAbility SoulAbility;

    private GameObject _aliveGameObject;
    private SoulAnimationToComponent _soulAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    protected bool IsUsingAbility;
    protected bool HasUsedAbility;
    protected bool ShouldStartUsingAbility;
    protected Vector2[] AbilityDirections { get; private set; }
    protected int AbilityDirectionIndex;
    private float _abilityCooldown;
    private float _startTime;

    protected virtual void Awake()
    {
        _aliveGameObject = transform.Find("Alive").gameObject;
        MyAnimator = _aliveGameObject.GetComponent<Animator>();
        _soulAnimationToComponent = _aliveGameObject.GetComponent<SoulAnimationToComponent>();

        _soulAnimationToComponent.Soul = this;

        AbilityDirections = SoulStats.directions;
        _abilityCooldown = SoulStats.abilityCooldown;

        _startTime = Time.time;

        transform.position += (Vector3)SoulStats.startPositionOffset;
    }

    protected virtual void Update()
    {
        if (Time.time > _startTime + _abilityCooldown)
        {
            IsUsingAbility = true;
        }
    }

    protected virtual void StartUsingAbility()
    {
        MyAnimator.SetBool("ability", true);

        transform.position = transform.position - (Vector3)SoulStats.startPositionOffset + (Vector3)SoulStats.abilityPositionOffset;

        for (AbilityDirectionIndex = 0; AbilityDirectionIndex < AbilityDirections.Length; AbilityDirectionIndex++)
        {
            UserAbility(AbilityDirections[AbilityDirectionIndex]);
        }
    }

    protected abstract void UserAbility(Vector2 abilityDirection);

    public virtual void StartUsingAbilityTrigger()
    {
        ShouldStartUsingAbility = true;
    }

    public virtual void FinishUsingAbilityTrigger()
    {
        HasUsedAbility = true;
    }

    protected bool CheckIfTouchingWall(float distance, Vector2 direction)
    {
        foreach (LayerMask layerMask in SoulStats.notAfectedLayerMasks)
        {
            if (Physics2D.Raycast(_aliveGameObject.transform.position, direction, distance, layerMask))
            {
                return true;
            }
        }

        return false;
    }
}
