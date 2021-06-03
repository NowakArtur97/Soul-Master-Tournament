using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardsData", menuName = "Data/Environment Hazard For Generation")]
public class D_EnvironmentHazard : ScriptableObject
{
    public GameObject environmentHazard;

    public float chanceForEnvironmentHazard;

    public bool isOnWall = false;

    public Vector2 environmentHazardOffset = new Vector2(0.5f, 0.55f);
}
