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
        GenerateFloors();
    }

    private void GenerateFloors()
    {
        Tile[] floors = _tilesData.floors;
        Vector3Int position = Vector3Int.zero;

        for (int row = 0; row < _tileMapRows; row++)
        {
            for (int column = 0; column < _tileMapColumns; column++)
            {
                position.Set(row, column, 0);
                _battleGround.SetTile(_offset + position, GetRandomTile(floors));
            }
        }
    }

    private static Tile GetRandomTile(Tile[] tiles) => tiles[Random.Range(0, tiles.Length)];
}
