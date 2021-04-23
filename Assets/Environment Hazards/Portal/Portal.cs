using UnityEngine;

public class Portal : EnvironmentHazardActiveOnContact
{
    [SerializeField]
    private Portal _connectedPortal;
    [SerializeField]
    private Vector2 _teleportationOffset = new Vector2(0, 1);

    private Coroutine _teleportCoroutine;

    protected override void UseEnvironmentHazard()
    {
        if (_toInteract)
        {
            _toInteract.transform.position = (Vector2)_connectedPortal.transform.position + _teleportationOffset;
        }
    }

    public void SetConnectedPortal(Portal connectedPortal) => _connectedPortal = connectedPortal;
}
