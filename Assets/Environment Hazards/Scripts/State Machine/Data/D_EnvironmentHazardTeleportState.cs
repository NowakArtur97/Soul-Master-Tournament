using UnityEngine;

[CreateAssetMenu(fileName = "_TeleportState", menuName = "Data/Environment Hazard Teleport State")]
public class D_EnvironmentHazardTeleportState : ScriptableObject
{
    public float teleportWaitTime = 5.0f;
    public Vector2 teleportationOffset = new Vector2(0, 1);
}
