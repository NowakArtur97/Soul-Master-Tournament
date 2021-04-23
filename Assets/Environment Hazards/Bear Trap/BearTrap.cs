using System.Collections;
using UnityEngine;

public class BearTrap : EnvironmentHazardActiveOnContact
{
    [SerializeField]
    private Vector2 _trappedOffset = new Vector2(0, 1);

    [SerializeField]
    private float immobilityTime = 2.0f;

    private Coroutine _trappedCoroutine;

    protected override void UseEnvironmentHazard()
    {
        if (_toInteract)
        {
            Debug.Log(_toInteract.name);
            Player player = _toInteract.GetComponentInParent<Player>();

            if (player != null)
            {
                player.PlayerStatsManager.Immobilize(immobilityTime);
                player.transform.position += (Vector3)_trappedOffset;

                if (_trappedCoroutine != null)
                {
                    StopCoroutine(_trappedCoroutine);
                }
                _trappedCoroutine = StartCoroutine(TrapPlayer());

                StopUsingEnvironmentHazardTrigger();
            }
        }
    }

    private IEnumerator TrapPlayer()
    {
        yield return new WaitForSeconds(immobilityTime);
    }
}
