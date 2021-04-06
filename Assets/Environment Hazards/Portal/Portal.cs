using System.Collections;
using UnityEngine;

public class Portal : EnvironmentHazard
{
    [SerializeField]
    private Portal _connectedPortal;
    [SerializeField]
    private float _timeBeforeTeleportation = 1.0f;

    private GameObject _toTeleport;
    private Coroutine _teleportCoroutine;

    private void Start()
    {
        SetIfEnvironmentHazardIsActivate(false);
    }

    protected override void UseEnvironmentHazard()
    {
        if (_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
        }
        IsActive = false;
        _teleportCoroutine = StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(_timeBeforeTeleportation);

        if (_toTeleport)
        {
            _toTeleport.transform.position = _connectedPortal.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartUsingEnvironmentHazardTrigger();
        _toTeleport = collision.gameObject;
        SetIfEnvironmentHazardIsActivate(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopUsingEnvironmentHazardTrigger();
        _toTeleport = null;
    }
}
