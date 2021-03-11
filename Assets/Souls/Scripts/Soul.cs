using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    [SerializeField]
    protected D_SoulStats SoulStats;
    [SerializeField]
    protected SoulAbility SoulAbility;

    protected GameObject AliveGameObject { get; private set; }
    private SoulAnimationToComponent _soulAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    protected bool IsUsingAbility;
    protected bool HasUsedAbility;
    protected bool HasMaxAbilityTimeFinished;
    protected bool ShouldStartUsingAbility;
    protected Vector2 AbilityDirection { get; private set; }
    protected Vector2[] AbilityDirections { get; private set; }
    protected int AbilityDirectionIndex;
    protected int AbilityRange;
    private float _abilityCooldown;
    private float _maxAbilityDuration;
    private bool _isAbilityTriggeredAfterTime;
    protected float StartTime;

    protected virtual void Awake()
    {
        AliveGameObject = transform.Find("Alive").gameObject;
        MyAnimator = AliveGameObject.GetComponent<Animator>();
        _soulAnimationToComponent = AliveGameObject.GetComponent<SoulAnimationToComponent>();

        _soulAnimationToComponent.Soul = this;

        AbilityDirections = SoulStats.directions;
        _abilityCooldown = SoulStats.abilityCooldown;
        _maxAbilityDuration = SoulStats.maxAbilityDuration;
        _isAbilityTriggeredAfterTime = SoulStats.isAbilityTriggeredAfterTime;
        AbilityRange = SoulStats.abilityRange;

        StartTime = Time.time;

        transform.position += (Vector3)SoulStats.startPositionOffset;
    }

    protected virtual void Update()
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

    protected virtual void StartUsingAbility()
    {
        MyAnimator.SetBool("ability", true);

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

        for (int range = 1; range <= AbilityRange; range++)
        {
            if (CheckIfTouchingWall(range, AbilityDirection, SoulStats.notAfectedLayerMasks))
            {
                return;
            }

            ability = Instantiate(SoulAbility, GetSoulPosition(range), GetSoulRotation());

            if (CheckIfTouchingObstacle(range, AbilityDirection, SoulStats.afectedLayerMasks))
            {
                range = AbilityRange;
            }

            ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(range), true);
        }
    }

    protected abstract Vector2 GetSoulPosition(int range);

    protected abstract Quaternion GetSoulRotation();

    protected abstract string GetAnimationBoolName(int range);

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

    public virtual void StartUsingAbilityTrigger() => ShouldStartUsingAbility = true;

    public virtual void FinishUsingAbilityTrigger() => HasUsedAbility = true;
}
