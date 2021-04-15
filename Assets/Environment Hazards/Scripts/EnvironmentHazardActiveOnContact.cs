using System.Collections;
using UnityEngine;

public abstract class EnvironmentHazardActiveOnContact : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveOnContactStats EnvironmentHazardData;

    private Coroutine _idleCoroutine;

    protected override void Update()
    {
        base.Update();

        if (CurrentStatus == Status.TRIGGERED)
        {
            if (_idleCoroutine != null)
            {
                StopCoroutine(_idleCoroutine);
            }
            _idleCoroutine = StartCoroutine(WaitBeforeAction());
        }
    }

    private IEnumerator WaitBeforeAction()
    {
        yield return new WaitForSeconds(EnvironmentHazardData.timeBeforeActivation);
        CurrentStatus = Status.ACTIVE;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) => CurrentStatus = Status.TRIGGERED;

    protected virtual void OnTriggerExit2D(Collider2D collision) => CurrentStatus = Status.FINISHED;
}
