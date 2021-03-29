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

    private void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        MyAnimator = AliveGameObject.GetComponent<Animator>();
        _environmentHazardAnimationToComponent = AliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        _environmentHazardAnimationToComponent.EnvironmentHazard = this;

        AttackDetails.damageAmmount = EnvironmentHazardData.damage;

        StartTime = Time.time;

        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);
    }

    private void Update()
    {
        if (Time.time >= StartTime + EnvironmentHazardData.idleTime)
        {
            ActivateEnvironmentHazard();
        }
    }

    protected virtual void ActivateEnvironmentHazard()
    {
        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
        MyAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, true);

        StartTime = 0;
    }

    public void ActivatedTrigger()
    {
        ActivateEnvironmentHazard();
    }
}
