using System;
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
    protected Vector2 AbilityDirection { get; private set; }
    protected Vector2[] AbilityDirections { get; private set; }
    protected int AbilityDirectionIndex;
    protected int AbilityRange;
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
        AbilityRange = SoulStats.abilityRange;

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
            AbilityDirection = AbilityDirections[AbilityDirectionIndex];
            UseAbility();
        }
    }

    protected virtual void UseAbility()
    {
        Vector2 abilityPosition;
        string animationBoolName;
        SoulAbility ability;

        for (int range = 1; range <= AbilityRange; range++)
        {
            abilityPosition = GetSoulPosition(range);

            if (CheckIfTouchingWall(range, AbilityDirection))
            {
                return;
            }

            ability = Instantiate(SoulAbility, abilityPosition, GetSoulRotation());

            animationBoolName = GetAnimationBoolName(range);

            ability.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);
        }
    }

    protected virtual Vector2 GetSoulPosition(int range) => (Vector2)transform.position + range * AbilityDirection;

    protected virtual Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    protected virtual string GetAnimationBoolName(int range) => range != AbilityRange ? "middle" : "end";

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

    public virtual void StartUsingAbilityTrigger() => ShouldStartUsingAbility = true;

    public virtual void FinishUsingAbilityTrigger() => HasUsedAbility = true;
}
