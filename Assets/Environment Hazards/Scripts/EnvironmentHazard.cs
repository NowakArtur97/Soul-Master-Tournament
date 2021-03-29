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

    protected float StartTime { get; private set; }

    private float _idleTime;

    private void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        MyAnimator = AliveGameObject.GetComponent<Animator>();
        _environmentHazardAnimationToComponent = AliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        _environmentHazardAnimationToComponent.EnvironmentHazard = this;

        AttackDetails.damageAmount = EnvironmentHazardData.damage;

        StartTime = Time.time;

        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);

        _idleTime = Random.Range(EnvironmentHazardData.minIdleTime, EnvironmentHazardData.maxIdleTime);
    }

    private void Update()
    {
        if (Time.time >= StartTime + _idleTime)
        {
            ActivateEnvironmentHazard();
        }
    }

    protected virtual void ActivateEnvironmentHazard()
    {
        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
        MyAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, true);

        StartTime = 0;
        _idleTime = Random.Range(EnvironmentHazardData.minIdleTime, EnvironmentHazardData.maxIdleTime);
    }

    public void ActivatedTrigger()
    {
        ActivateEnvironmentHazard();
    }
}
