using System.Collections;
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
            CurrentStatus = Status.EMPTY;
            SetIsAnimationActive(true);
        }
        else if (CurrentStatus == Status.ACTIVE)
        {
            UseEnvironmentHazard();
        }
        else if (CurrentStatus == Status.FINISHED)
        {
            SetIsAnimationActive(false);
            _idleCoroutine = StartCoroutine(WaitBeforeAction());
        }
    }

    protected override IEnumerator WaitBeforeAction()
    {
        CurrentStatus = Status.EMPTY;

        yield return new WaitForSeconds(_idleTime);

        CurrentStatus = Status.TRIGGERED;

        StopCoroutine(_idleCoroutine);
    }
}
