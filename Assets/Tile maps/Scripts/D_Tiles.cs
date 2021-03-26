using UnityEngine;

[CreateAssetMenu(fileName = "_TilesData", menuName = "Data/Tiles/Data")]
public class D_Tiles : ScriptableObject
{
    [Header("Corners")]
    public GameObject upperLeftCorner;
    public GameObject upperRightCorner;
    public GameObject lowerLeftCorner;
    public GameObject lowerRightCorner;

    [Header("Walls")]
    public GameObject[] upperWalls;
    public GameObject[] lowerWalls;
    public GameObject[] leftWalls;
    public GameObject[] rightWalls;

    [Header("Floor")]
    public GameObject[] floors;
}
