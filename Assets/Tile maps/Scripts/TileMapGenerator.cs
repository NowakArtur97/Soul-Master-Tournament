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
        int wallIndex = 0;

        for (int column = 0; column < _tileMapColumns; column++)
        {
            for (int row = 0; row < _tileMapRows; row++)
            {
                position.Set(column, -row, 0);

                if (row == 0 && column == 0)
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.upperLeftCorner);
                }
                else if (row == 0 && column == _tileMapColumns - 1)
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.upperRightCorner);
                }
                else if (row == _tileMapRows - 1 && column == 0)
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.lowerLeftCorner);
                }
                else if (row == _tileMapRows - 1 && column == _tileMapColumns - 1)
                {
                    _outerWalls.SetTile(_offset + position, _tilesData.lowerRightCorner);
                }
                //else if (row > 0 && row < _tileMapRows - 1 && column == 0)
                //{
                //    _outerWalls.SetTile(_offset + position, _tilesData.upperWalls[wallIndex]);
                //    wallIndex++;
                //}
                //else if (row > 0 && row < _tileMapRows - 1 && column == _tileMapColumns - 1)
                //{
                //    _outerWalls.SetTile(_offset + position, _tilesData.upperWalls[wallIndex]);
                //    wallIndex++;
                //}

                if (wallIndex >= _tilesData.upperWalls.Length)
                {
                    wallIndex = 0;
                }
            }
        }
    }

    private static Tile GetRandomTile(Tile[] tiles) => tiles[Random.Range(0, tiles.Length)];
}
