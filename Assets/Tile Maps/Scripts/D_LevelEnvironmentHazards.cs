using UnityEngine;

[CreateAssetMenu(fileName = "_LevelEnvironmentHazardsData", menuName = "Data/Level Environment Hazards")]
public class D_LevelEnvironmentHazards : ScriptableObject
{
    [Header("Chances for obstacles or environment hazards")]
    public int chanceForObstacle = 70;
    public int chanceForEnvironmentHazards = 20;
    public int chanceForEnvironmentHazardsWithRandomRotation = 5;
    public int chanceForPortals = 10;
    public int chanceForSlidingSaw = 5;

    [Header("Obstacles and environment hazards")]
    public GameObject[] environmentHazards;

    public GameObject[] environmentHazardsWithRandomRotation;

    [Header("Sliding Saw with Rails")]
    public GameObject slidingSaw;
    public GameObject leftEndRail;
    public GameObject rightEndRail;
    public GameObject middleRail;

    [Header("Other")]
    public GameObject portal;
}
