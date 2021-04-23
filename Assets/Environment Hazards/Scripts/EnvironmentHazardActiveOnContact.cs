using UnityEngine;

public abstract class EnvironmentHazardActiveOnContact : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveOnContactStats EnvironmentHazardData;

    protected GameObject _toInteract;

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

        if (!EnvironmentHazardData.isActiveOnTrigger)
        {
            IdleCoroutine = StartCoroutine(WaitBeforeAction(EnvironmentHazardData.timeBeforeActivation, Status.ACTIVE));
        }
        else
        {
            // Wait for Animation Trigger
            CurrentStatus = Status.EMPTY;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentStatus = Status.TRIGGERED;
        _toInteract = collision.gameObject;
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        CurrentStatus = Status.FINISHED;
        _toInteract = null;
    }
}
