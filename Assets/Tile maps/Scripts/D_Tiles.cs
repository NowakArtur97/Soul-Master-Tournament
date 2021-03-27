using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "_TilesData", menuName = "Data/Tiles/Data")]
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
}
