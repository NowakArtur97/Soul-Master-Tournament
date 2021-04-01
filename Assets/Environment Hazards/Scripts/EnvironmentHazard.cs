using UnityEngine;

public abstract class EnvironmentHazard : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ACTIVE_ANIMATION_BOOL_NAME = "active";

    [SerializeField]
    protected D_EnvironmentHazardStats EnvironmentHazardData;

    protected GameObject AliveGameObject { get; private set; }
    private EnvironmentHazardAnimationToComponent _environmentHazardAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    protected AttackDetails AttackDetails;

    protected bool IsFinished;
    private float _activeTime;
    private float _idleTime;

    private void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        MyAnimator = AliveGameObject.GetComponent<Animator>();
        _environmentHazardAnimationToComponent = AliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        _environmentHazardAnimationToComponent.EnvironmentHazard = this;

        AttackDetails.damageAmount = EnvironmentHazardData.damage;

        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);

        _idleTime = Random.Range(EnvironmentHazardData.minIdleTime, EnvironmentHazardData.maxIdleTime);
        _activeTime = Time.time;
        IsFinished = true;
    }

    private void Update()
    {
        if (Time.time >= _activeTime + _idleTime && IsFinished)
        {
            IsFinished = false;
            SetIfEnvironmentHazardIsActivate(true);
            _activeTime = Time.time;
        }
    }

    protected void SetIfEnvironmentHazardIsActivate(bool isActive)
    {
        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isActive);
        MyAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, isActive);
    }

    protected abstract void UseEnvironmentHazard();

    protected void StopUsingEnvironmentHazard()
    {
        IsFinished = true;
        SetIfEnvironmentHazardIsActivate(false);
    }

    public void UseEnvironmentHazardTrigger() => UseEnvironmentHazard();

    public void StopUsingEnvironmentHazardTrigger() => StopUsingEnvironmentHazard();
}
