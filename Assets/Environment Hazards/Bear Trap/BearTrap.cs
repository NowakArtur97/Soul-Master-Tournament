using System.Collections;
using UnityEngine;

public class BearTrap : EnvironmentHazardActiveOnContact
{
    [SerializeField]
    private Vector2 _afterBeingTrappedOffset = new Vector2(0, 0.3f);

    [SerializeField]
    private float _immobilityTime = 2.0f;

    private Coroutine _trappedCoroutine;

    protected override void UseEnvironmentHazard()
    {
        if (ToInteract)
        {
            Player player = ToInteract.GetComponentInParent<Player>();

            if (player != null)
            {
                PlayerStatus immobilizedStatus = new ImmobilizedStatus(_immobilityTime);
                player.AddStatus(immobilizedStatus);
                ToInteract.transform.position = gameObject.transform.position + (Vector3)_afterBeingTrappedOffset;
            }
        }

        StopUsingEnvironmentHazardTrigger();

        if (_trappedCoroutine != null)
        {
            StopCoroutine(_trappedCoroutine);
        }

        _trappedCoroutine = StartCoroutine(TrapPlayer());
    }

    protected override void FinishUsingEnvironmentHazard()
    {
        if (IdleCoroutine != null)
        {
            StopCoroutine(IdleCoroutine);
        }
    }

    private IEnumerator TrapPlayer()
    {
        yield return new WaitForSeconds(_immobilityTime);

        SetIsAnimationActive(false);
    }
}
