using UnityEngine;

public abstract class EnvironmentHazardActiveAfterTime : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveAfterTimeStats EnvironmentHazardData;

    private float _idleTime;

    protected override void Awake()
    {
        base.Awake();

        _idleTime = Random.Range(EnvironmentHazardData.minIdleTime, EnvironmentHazardData.maxIdleTime);
    }

    private void Update()
    {
        if (CurrentStatus == Status.TRIGGERED)
        {
            TriggerEnvironmentHazard();
        }
        else if (CurrentStatus == Status.ACTIVE)
        {
            UseEnvironmentHazard();
        }
        else if (CurrentStatus == Status.FINISHED)
        {
            FinishUsingEnvironmentHazard();
        }
    }

    protected override void TriggerEnvironmentHazard()
    {
        base.TriggerEnvironmentHazard();

        CurrentStatus = Status.ACTIVE;
    }

    protected override void FinishUsingEnvironmentHazard()
    {
        base.FinishUsingEnvironmentHazard();

        if (IdleCoroutine != null)
        {
            StopCoroutine(IdleCoroutine);
        }
        IdleCoroutine = StartCoroutine(WaitBeforeAction(_idleTime, Status.TRIGGERED));
    }
}
