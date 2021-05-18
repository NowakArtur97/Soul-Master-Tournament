using UnityEngine;

[CreateAssetMenu(fileName = "_EnvironmentHazardsData", menuName = "Data/Environment Hazard")]
public class D_EnvironmentHazard : ScriptableObject
{
    public GameObject environmentHazard;

    public float chanceForEnvironmentHazard;

    public bool isOnWall = false;
}
