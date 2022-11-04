using UnityEngine;

[CreateAssetMenu(fileName = "_TrapPlayerStateData", menuName = "Data/Environment Hazard Trap Player State")]
public class D_EnvironmentHazardTrapPlayerState : ScriptableObject
{
    public float immobilityTime = 2.0f;
    public Vector2 afterBeingTrappedOffset = new Vector2(-0.05f, 1.0f);
}
