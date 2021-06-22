using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    protected const string SUMMON_ANIMATION_BOOL_NAME = "summon";
    protected const string UNSUMMON_ANIMATION_BOOL_NAME = "unsummon";
    protected const string ABILITY_ANIMATION_BOOL_NAME = "ability";

    protected const string ABILITY_CLIP_TITLE = "_Ability";
    protected const string SUMMON_CLIP_TITLE = "SoulSummon";
    protected const string UNSUMMON_CLIP_TITLE = "SoulUnsummon";

    [SerializeField]
    protected D_SoulStats SoulStats;
    [SerializeField]
    protected SoulAbility SoulAbility;

    protected Player Player { get; private set; }
    protected GameObject AliveGameObject { get; private set; }
    private SoulAnimationToComponent _soulAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    protected bool HasAppeared { get; private set; }
    protected bool IsSummoned { get; private set; }
    private string _soulName;
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

        _soulName = GetType().Name;
    }

    protected virtual void Update()
    {
        if (IsSummoned)
        {
            if (IsAbilityCooldownOver() && _isAbilityTriggeredAfterTime)
            {
                IsUsingAbility = true;
            }
            if (IsMaxAbilityDurationFinished())
            {
                HasMaxAbilityTimeFinished = true;
            }
            if (Player == null)
            {
                DisableAllBoolParametersInAnimator();
                UnsummonSoul();
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
            if (CheckIfTouching(AbilityRange, AbilityDirection, SoulStats.notAfectedLayerMasks))
            {
                return;
            }

            ability = Instantiate(SoulAbility, GetSoulPosition(), GetSoulRotation());

            if (CheckIfTouching(AbilityRange, AbilityDirection, SoulStats.afectedLayerMasks))
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
            if (SoulStats.canPlayerSummonAfterDestroy)
            {
                Player.LetPlacingSouls();
            }
            Destroy(gameObject);
        }
    }

    protected abstract Vector2 GetSoulPosition();

    protected abstract Quaternion GetSoulRotation();

    // Empty for abilities without animators
    protected virtual string GetAnimationBoolName() => "";

    protected bool CheckIfTouching(float distance, Vector2 direction, LayerMask[] layerMasks)
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

    public virtual void PlaySummonSoundTrigger() => AudioManager.Instance.Play(SUMMON_CLIP_TITLE);

    public virtual void PauseSummonSoundTrigger() => AudioManager.Instance.Pause(SUMMON_CLIP_TITLE);

    public virtual void PlayUnsummonSoundTrigger() => AudioManager.Instance.Play(UNSUMMON_CLIP_TITLE);

    public virtual void PauseUnsummonSoundTrigger() => AudioManager.Instance.Pause(UNSUMMON_CLIP_TITLE);

    public virtual void PlayAbilitySoundTrigger() => AudioManager.Instance.Play(_soulName + ABILITY_CLIP_TITLE);

    public virtual void PauseAbilitySoundTrigger() => AudioManager.Instance.Pause(_soulName + ABILITY_CLIP_TITLE);

    private void DisableAllBoolParametersInAnimator()
    {
        foreach (AnimatorControllerParameter parameter in MyAnimator.parameters)
        {
            MyAnimator.SetBool(parameter.name, false);
        }
    }

    public void SetPlayer(Player player) => Player = player;

    private bool IsAbilityCooldownOver() => Time.time >= StartTime + _abilityCooldown;

    private bool IsMaxAbilityDurationFinished() => Time.time >= StartTime + _maxAbilityDuration;
}
