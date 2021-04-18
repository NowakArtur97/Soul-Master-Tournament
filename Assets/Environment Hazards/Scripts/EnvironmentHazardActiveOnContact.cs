using System.Collections;
using UnityEngine;

public abstract class EnvironmentHazardActiveOnContact : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveOnContactStats EnvironmentHazardData;

    private void Update()
    {
        if (CurrentStatus == Status.TRIGGERED)
        {
            SetIsAnimationActive(true);

            _idleCoroutine = StartCoroutine(WaitBeforeAction());
        }
        else if (CurrentStatus == Status.ACTIVE)
        {
            UseEnvironmentHazard();
        }
        else if (CurrentStatus == Status.FINISHED)
        {
            SetIsAnimationActive(false);

            if (_idleCoroutine != null)
            {
                StopCoroutine(_idleCoroutine);
            }
        }
    }

    protected override IEnumerator WaitBeforeAction()
    {
        CurrentStatus = Status.EMPTY;

        yield return new WaitForSeconds(EnvironmentHazardData.timeBeforeActivation);

        CurrentStatus = Status.ACTIVE;

        StopCoroutine(_idleCoroutine);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) => CurrentStatus = Status.TRIGGERED;

    protected virtual void OnTriggerExit2D(Collider2D collision) => CurrentStatus = Status.FINISHED;
}
