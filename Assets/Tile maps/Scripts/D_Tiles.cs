using UnityEngine;

[CreateAssetMenu(fileName = "_TilesData", menuName = "Data/Tiles/Data")]
public class D_Tiles : ScriptableObject
{
    [Header("Corners")]
    public GameObject _upperLeftCorner;
    public GameObject _upperRightCorner;
    public GameObject _lowerLeftCorner;
    public GameObject _lowerRightCorner;

    [Header("Walls")]
    public GameObject _upperWall;
    public GameObject _lowerWall;
    public GameObject _leftWall;
    public GameObject _rightWall;

    [Header("Floor")]
    public GameObject[] _floors;
}
