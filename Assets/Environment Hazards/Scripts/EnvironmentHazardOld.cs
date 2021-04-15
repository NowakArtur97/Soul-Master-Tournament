using UnityEngine;

// TODO: Remove old script
public abstract class EnvironmentHazardOld : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ACTIVE_ANIMATION_BOOL_NAME = "active";

    [SerializeField]
    protected D_EnvironmentHazardStats EnvironmentHazardData;

    protected GameObject AliveGameObject { get; private set; }
    private EnvironmentHazardAnimationToComponent _environmentHazardAnimationToComponent;
    protected Animator MyAnimator { get; private set; }
    protected Rigidbody2D MyRigidbody2D { get; private set; }

    protected bool IsFinished;
    protected bool IsActive;
    private float _activeTime;
    private float _idleTime;

    private void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        MyAnimator = AliveGameObject.GetComponent<Animator>();
        MyRigidbody2D = AliveGameObject.GetComponent<Rigidbody2D>();
        _environmentHazardAnimationToComponent = AliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        if (_environmentHazardAnimationToComponent)
        {
            //_environmentHazardAnimationToComponent.EnvironmentHazard = this;
        }

        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);

        //_idleTime = Random.Range(EnvironmentHazardData.minIdleTime, EnvironmentHazardData.maxIdleTime);
        _activeTime = Time.time;
        IsFinished = true;
        IsActive = false;
    }

    private void Update()
    {
        if (IsIdleTimeOver() && IsFinished)
        //&& EnvironmentHazardData.shouldActivateAfterTime)
        {
            IdleTimeIsOver();
        }

        if (IsActive)
        {
            UseEnvironmentHazard();
        }
    }

    protected virtual void IdleTimeIsOver()
    {
        IsFinished = false;
        SetIfEnvironmentHazardIsActivate(true);
        _activeTime = Time.time;
    }

    protected abstract void UseEnvironmentHazard();

    private void StartUsingEnvironmentHazard() => IsActive = true;

    private void StopUsingEnvironmentHazard()
    {
        IsFinished = true;
        IsActive = false;
        SetIfEnvironmentHazardIsActivate(false);
        _activeTime = Time.time;
    }

    protected void SetIfEnvironmentHazardIsActivate(bool isActive)
    {
        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isActive);
        MyAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, isActive);
    }

    public void StartUsingEnvironmentHazardTrigger() => StartUsingEnvironmentHazard();

    public void StopUsingEnvironmentHazardTrigger() => StopUsingEnvironmentHazard();

    protected bool IsIdleTimeOver() => Time.time >= _activeTime + _idleTime;
}
