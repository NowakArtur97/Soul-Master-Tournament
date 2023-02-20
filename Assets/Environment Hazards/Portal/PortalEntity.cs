using UnityEngine;

public class PortalEntity : EnvironmentHazardEntity
{
    [SerializeField] private D_EnvironmentHazardTeleportState _teleportStateData;
    public D_EnvironmentHazardTeleportState TeleportStateData
    {
        get { return _teleportStateData; }
        private set { _teleportStateData = value; }
    }
    [SerializeField] private PortalEntity _connectedPortal;
    public PortalEntity ConnectedPortal
    {
        get { return _connectedPortal; }
        private set { _connectedPortal = value; }
    }

    protected override void Awake()
    {
        IdleState = new IdleOnContactState(this, "idle", IdleStateData);
        ActiveState = new TeleportState(this, "active", _teleportStateData);
        PlayerDetectedState = new PlayerDetectedState(this, "idle", PlayerDetectedStateData);

        base.Awake();
    }

    public void SetConnectedPortal(PortalEntity connectedPortal) => _connectedPortal = connectedPortal;

    protected override bool HasSpecificTag(GameObject collisionGameObject) => collisionGameObject.tag == PLAYER_GAME_TAG;
}
