using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    protected const string SUMMON_ANIMATION_BOOL_NAME = "summon";
    protected const string UNSUMMON_ANIMATION_BOOL_NAME = "unsummon";
    protected const string ABILITY_ANIMATION_BOOL_NAME = "ability";
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField]
    protected D_SoulStats SoulStats;
    [SerializeField]
    protected SoulAbility SoulAbility;

    protected GameObject AliveGameObject { get; private set; }
    private SoulAnimationToComponent _soulAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    protected bool HasAppeared { get; private set; }
    protected bool IsSummoned { get; private set; }
    private bool _isUnsummoned;
    protected bool IsUsingAbility;
    protected bool HasUsedAbility;
    protected bool HasMaxAbilityTimeFinished { get; private set; }
    protected bool ShouldStartUsingAbility;
    protected Vector2 AbilityDirection { get; private set; }
    protected Vector2[] AbilityDirections { get; private set; }
    protected int AbilityDirectionIndex { get; private set; }
    protected int AbilityRange { get; private set; }
    protected int AbilityMaxRange { get; private set; }
    private float _abilityCooldown;
    private float _maxAbilityDuration;
    private bool _isAbilityTriggeredAfterTime;
    protected float StartTime { get; private set; }

    protected virtual void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        MyAnimator = AliveGameObject.GetComponent<Animator>();
        _soulAnimationToComponent = AliveGameObject.GetComponent<SoulAnimationToComponent>();

        _soulAnimationToComponent.Soul = this;

        AbilityDirections = SoulStats.directions;
        _abilityCooldown = SoulStats.abilityCooldown;
        _maxAbilityDuration = SoulStats.maxAbilityDuration;
        _isAbilityTriggeredAfterTime = SoulStats.isAbilityTriggeredAfterTime;
        AbilityMaxRange = SoulStats.abilityRange;

        StartTime = Time.time;

        transform.position += (Vector3)SoulStats.startPositionOffset;

        MyAnimator.SetBool(SUMMON_ANIMATION_BOOL_NAME, true);
    }

    protected virtual void Update()
    {
        if (IsSummoned)
        {
            if (Time.time >= StartTime + _abilityCooldown && _isAbilityTriggeredAfterTime)
            {
                IsUsingAbility = true;
            }
            if (Time.time >= StartTime + _maxAbilityDuration)
            {
                HasMaxAbilityTimeFinished = true;
            }
        }
    }

    protected virtual void StartUsingAbility()
    {
        MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);

        transform.position = transform.position - (Vector3)SoulStats.startPositionOffset + (Vector3)SoulStats.abilityPositionOffset;

        for (AbilityDirectionIndex = 0; AbilityDirectionIndex < AbilityDirections.Length; AbilityDirectionIndex++)
        {
            AbilityDirection = AbilityDirections[AbilityDirectionIndex];
            UseAbility();
        }
    }

    protected virtual void UseAbility()
    {
        SoulAbility ability;

        for (AbilityRange = 1; AbilityRange <= AbilityMaxRange; AbilityRange++)
        {
            if (CheckIfTouchingWall(AbilityRange, AbilityDirection, SoulStats.notAfectedLayerMasks))
            {
                return;
            }

            ability = Instantiate(SoulAbility, GetSoulPosition(), GetSoulRotation());

            if (CheckIfTouchingObstacle(AbilityRange, AbilityDirection, SoulStats.afectedLayerMasks))
            {
                AbilityRange = AbilityMaxRange;
            }

            ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(), true);
        }
    }

    protected virtual void FinishSummoningSoul(string nextAnimationBoolName)
    {
        MyAnimator.SetBool(SUMMON_ANIMATION_BOOL_NAME, false);
        MyAnimator.SetBool(nextAnimationBoolName, true);
        HasAppeared = true;
    }

    protected void UnsummonSoul()
    {
        transform.position = transform.position - (Vector3)SoulStats.abilityPositionOffset + (Vector3)SoulStats.startPositionOffset;
        MyAnimator.SetBool(UNSUMMON_ANIMATION_BOOL_NAME, true);

        if (_isUnsummoned)
        {
            Destroy(gameObject);
        }
    }

    protected abstract Vector2 GetSoulPosition();

    protected abstract Quaternion GetSoulRotation();

    protected abstract string GetAnimationBoolName();

    protected bool CheckIfTouchingWall(float distance, Vector2 direction, LayerMask[] notAfectedLayerMasks) =>
        CheckIfTouching(distance, direction, notAfectedLayerMasks);

    protected bool CheckIfTouchingObstacle(float distance, Vector2 direction, LayerMask[] afectedLayerMasks) =>
        CheckIfTouching(distance, direction, afectedLayerMasks);

    private bool CheckIfTouching(float distance, Vector2 direction, LayerMask[] layerMasks)
    {
        foreach (LayerMask layerMask in layerMasks)
        {
            if (Physics2D.Raycast(AliveGameObject.transform.position, direction, distance, layerMask))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void SummonedTrigger() => IsSummoned = true;

    public virtual void UnsummonedTrigger() => _isUnsummoned = true;

    public virtual void StartUsingAbilityTrigger() => ShouldStartUsingAbility = true;

    public virtual void FinishUsingAbilityTrigger() => HasUsedAbility = true;
}
