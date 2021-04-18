using UnityEngine;

public abstract class EnvironmentHazardActiveOnContact : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveOnContactStats EnvironmentHazardData;

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

        IdleCoroutine = StartCoroutine(WaitBeforeAction(EnvironmentHazardData.timeBeforeActivation, Status.ACTIVE));
    }

    protected override void FinishUsingEnvironmentHazard()
    {
        base.FinishUsingEnvironmentHazard();

        if (IdleCoroutine != null)
        {
            StopCoroutine(IdleCoroutine);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) => CurrentStatus = Status.TRIGGERED;

    protected virtual void OnTriggerExit2D(Collider2D collision) => CurrentStatus = Status.FINISHED;
}
