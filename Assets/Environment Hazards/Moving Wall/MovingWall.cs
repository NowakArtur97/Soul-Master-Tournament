using System.Collections;
using UnityEngine;

public class MovingWall : EnvironmentHazardActiveAfterTime
{
    private readonly string ACTIVATED_SOUND_CLIP = "MovingWall_Actived";

    [SerializeField]
    private float _blockingTime = 3f;

    private bool _isBlocking;

    private BoxCollider2D _myBoxCollider2D;

    private Coroutine _blockingCorouting;

    protected override void Awake()
    {
        base.Awake();

        _myBoxCollider2D = AliveGameObject.GetComponent<BoxCollider2D>();

        _myBoxCollider2D.enabled = false;
    }

    protected override void TriggerEnvironmentHazard()
    {
        AudioManager.Instance.Play(ACTIVATED_SOUND_CLIP);

        base.TriggerEnvironmentHazard();
    }

    protected override void UseEnvironmentHazard()
    {
        if (_isBlocking)
        {
            return;
        }

        _myBoxCollider2D.enabled = true;

        _isBlocking = true;

        if (_blockingCorouting != null)
        {
            StopCoroutine(_blockingCorouting);
        }
        _blockingCorouting = StartCoroutine(HideWall());
    }

    private IEnumerator HideWall()
    {
        yield return new WaitForSeconds(_blockingTime);

        _isBlocking = false;

        SetIsAnimationActive(false);
    }

    protected override void FinishUsingEnvironmentHazard()
    {
        base.FinishUsingEnvironmentHazard();

        _myBoxCollider2D.enabled = false;
    }
}
