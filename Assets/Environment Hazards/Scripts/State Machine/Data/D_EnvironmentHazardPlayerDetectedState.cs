using UnityEngine;

[CreateAssetMenu(fileName = "_PlayerDetectedStateData", menuName = "Data/Environment Hazard Player Detected State")]
public class D_EnvironmentHazardPlayerDetectedState : ScriptableObject
{
    public float timeToWaitAfterDetectingPlayer = 2.0f;
}
