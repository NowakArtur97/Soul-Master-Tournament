using System.Collections;
using UnityEngine;

public abstract class EnvironmentHazardActiveAfterTime : EnvironmentHazard
{
    [SerializeField]
    protected D_EnvironmentHazardActiveAfterTimeStats EnvironmentHazardData;

    private Coroutine _idleCoroutine;
    private float _idleTime;

    private void Start()
    {
        _idleTime = Random.Range(EnvironmentHazardData.minIdleTime, EnvironmentHazardData.maxIdleTime);
    }

    protected override void Update()
    {
        base.Update();

        if (CurrentStatus == Status.FINISHED)
        {
            _idleCoroutine = StartCoroutine(Idle());
        }
    }

    private IEnumerator Idle()
    {
        CurrentStatus = Status.EMPTY;

        yield return new WaitForSeconds(_idleTime);

        CurrentStatus = Status.TRIGGERED;

        StopCoroutine(_idleCoroutine);
    }
}
