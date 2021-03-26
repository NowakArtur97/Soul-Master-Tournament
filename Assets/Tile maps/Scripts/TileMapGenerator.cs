using UnityEngine;

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
    private Vector2 _offset = new Vector2(-3.5f, 4.4f);

    [Header("Tiles Data")]
    [SerializeField]
    private D_Tiles _tilesData;

    private GameObject _outerWalls;
    private GameObject _battleGround;
    private GameObject _obstacles;

    private void Awake()
    {
        _outerWalls = transform.Find(OUTER_WALLS_GAME_OBJECT_NAME).gameObject;
        _battleGround = transform.Find(BATTLE_GROUND_GAME_OBJECT_NAME).gameObject;
        _obstacles = transform.Find(OBSTACLES_GAME_OBJECT_NAME).gameObject;
    }

    private void Start()
    {
        GenerateFloors();
    }

    private void GenerateFloors()
    {
        GameObject tile;
        GameObject[] floors = _tilesData.floors;
        Vector2 position = Vector2.zero;

        for (int row = 0; row < _tileMapRows; row++)
        {
            for (int column = 0; column < _tileMapColumns; column++)
            {
                position.Set(row, column);
                tile = Instantiate(floors[Random.Range(0, floors.Length)], _offset + position, Quaternion.identity);
                tile.transform.parent = _battleGround.transform;
            }
        }
    }
}
