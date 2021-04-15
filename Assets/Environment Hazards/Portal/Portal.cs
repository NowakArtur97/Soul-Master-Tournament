using System.Collections;
using UnityEngine;

public class Portal : EnvironmentHazardActiveOnContact
{
    [SerializeField]
    private Portal _connectedPortal;
    [SerializeField]
    private Vector2 _teleportationOffset = new Vector2(0, 1);

    private GameObject _toTeleport;
    private Coroutine _teleportCoroutine;

    protected override void UseEnvironmentHazard()
    {
        if (_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
        }
        _teleportCoroutine = StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(EnvironmentHazardData.timeBeforeActivation);

        if (_toTeleport)
        {
            _toTeleport.transform.position = (Vector2)_connectedPortal.transform.position + _teleportationOffset;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        _toTeleport = collision.gameObject;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        _toTeleport = collision.gameObject;
    }

    public void SetConnectedPortal(Portal connectedPortal) => _connectedPortal = connectedPortal;
}
