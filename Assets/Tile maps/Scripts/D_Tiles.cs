using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "_TilesData", menuName = "Data/Tiles")]
public class D_Tiles : ScriptableObject
{
    [Header("Corners")]
    public Tile upperLeftCorner;
    public Tile upperRightCorner;
    public Tile lowerLeftCorner;
    public Tile lowerRightCorner;

    [Header("Walls")]
    public Tile[] upperWalls;
    public Tile[] lowerWalls;
    public Tile[] leftWalls;
    public Tile[] rightWalls;

    public Tile[] floors;

    public Tile[] obstacles;

    [Header("Chances for obstacles or environment hazards")]
    public int chanceForObstacle = 60;
    public int chanceForEnvironmentHazards = 10;
    public int chanceForEnvironmentHazardsWithRandomRotation = 7;
    public int chanceForPortals = 5;

    [Header("Obstacles and environment hazards")]
    public GameObject[] environmentHazards;

    public GameObject[] environmentHazardsWithRandomRotation;

    public GameObject portal;
}
