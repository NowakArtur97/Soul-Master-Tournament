using UnityEngine;

[CreateAssetMenu(fileName = "_BlockedStateData", menuName = "Data/Environment Hazard Blocked State")]
public class D_EnvironmentHazardBlockedState : ScriptableObject
{
    public float isBlockingDistanceCheck;
    public Vector2 isBlockingSizeCheck = Vector2.one;
    public LayerMask whatIsBlocking;
}
