using UnityEngine;

public abstract class EnvironmentHazardActiveAfterTime : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveAfterTimeStats EnvironmentHazardData;

    private float _idleTime;

    private void Start()
    {
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

        CurrentStatus = Status.EMPTY;
    }

    protected override void FinishUsingEnvironmentHazard()
    {
        base.FinishUsingEnvironmentHazard();

        IdleCoroutine = StartCoroutine(WaitBeforeAction(_idleTime, Status.TRIGGERED));
    }
}
