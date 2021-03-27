using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    private const string OUTER_WALLS_GAME_OBJECT_NAME = "Outer Walls";
    private const string BATTLE_GROUND_GAME_OBJECT_NAME = "Battle Ground";
    private const string OBSTACLES_GAME_OBJECT_NAME = "Obstacles";

    [Header("Tile Map Size")]
    [SerializeField]
    private int _tileMapRows = 16;
    [SerializeField]
    private int _tileMapColumns = 16;

    [Header("Tile Map Position Offset")]
    [SerializeField]
    private Vector3Int _offset = new Vector3Int(-3, -11, 0);

    [Header("Tiles Data")]
    [SerializeField]
    private D_Tiles _tilesData;

    private Tilemap _outerWalls;
    private Tilemap _battleGround;
    private Tilemap _obstacles;

    private void Awake()
    {
        _outerWalls = transform.Find(OUTER_WALLS_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
        _battleGround = transform.Find(BATTLE_GROUND_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
        _obstacles = transform.Find(OBSTACLES_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
    }

    private void Start()
    {
        GenerateTileMaps();
    }

    private void GenerateTileMaps()
    {
        Vector3Int position = Vector3Int.zero;
        int wallsIndex = 0;
        int leftSideWallsIndex = 0;
        int rightSideWallsIndex = 0;

        for (int column = 0; column < _tileMapColumns; column++)
        {
            for (int row = 0; row < _tileMapRows; row++)
            {
                position.Set(column, -row, 0);

                if (IsFirstRow(row) && IsFirstColumn(column))
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.upperLeftCorner);
                }
                else if (IsFirstRow(row) && IsLastColumn(column))
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.upperRightCorner);
                }
                else if (IsLastRow(row) && IsFirstColumn(column))
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.lowerLeftCorner);
                }
                else if (IsLastRow(row) && IsLastColumn(column))
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.lowerRightCorner);
                }
                else if (IsBetweenCorners(column))
                {
                    if (IsFirstRow(row))
                    {
                        _outerWalls.SetTile(_offset + position, _tilesData.upperWalls[wallsIndex]);
                        wallsIndex++;
                    }
                    else if (IsLastRow(row))
                    {
                        _outerWalls.SetTile(_offset + position, _tilesData.lowerWalls[wallsIndex]);
                        wallsIndex++;
                    }
                    else
                    {
                        _battleGround.SetTile(_offset + position, GetRandomTile(_tilesData.floors));
                    }
                }
                else
                {
                    if (IsFirstColumn(column))
                    {
                        _outerWalls.SetTile(_offset + position, _tilesData.leftWalls[leftSideWallsIndex]);
                        leftSideWallsIndex++;
                    }
                    if (IsLastColumn(column))
                    {
                        _outerWalls.SetTile(_offset + position, _tilesData.rightWalls[rightSideWallsIndex]);
                        rightSideWallsIndex++;
                    }
                }

                if (wallsIndex >= _tilesData.upperWalls.Length)
                {
                    wallsIndex = 0;
                }
                if (leftSideWallsIndex >= _tilesData.leftWalls.Length)
                {
                    leftSideWallsIndex = 0;
                }
                if (rightSideWallsIndex >= _tilesData.rightWalls.Length)
                {
                    rightSideWallsIndex = 0;
                }
            }
        }
    }

    private bool IsBetweenCorners(int column) => column > 0 && column < _tileMapColumns - 1;

    private bool IsFirstColumn(int column) => column == 0;

    private bool IsLastColumn(int column) => column == _tileMapColumns - 1;

    private bool IsFirstRow(int row) => row == 0;

    private bool IsLastRow(int row) => row == _tileMapRows - 1;

    private static Tile GetRandomTile(Tile[] tiles) => tiles[Random.Range(0, tiles.Length)];
}
