using UnityEngine;

public abstract class EnvironmentHazardActiveOnContact : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveOnContactStats EnvironmentHazardData;

    protected GameObject ToInteract;
    protected float TimeBeforeActivation;
    private Status _statusAfterWaiting;

    protected override void Awake()
    {
        base.Awake();

        TimeBeforeActivation = EnvironmentHazardData.timeBeforeActivation;
        _statusAfterWaiting = EnvironmentHazardData.isActiveOnTrigger ? Status.EMPTY : Status.ACTIVE;
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

        IdleCoroutine = StartCoroutine(WaitBeforeAction(TimeBeforeActivation, _statusAfterWaiting));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentStatus = Status.TRIGGERED;
        ToInteract = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrentStatus = Status.FINISHED;
        ToInteract = null;
    }
}
