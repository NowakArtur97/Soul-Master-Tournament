using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class TileMapGenerator : MonoBehaviour
{
    private const string OUTER_WALLS_GAME_OBJECT_NAME = "Outer Walls";
    private const string BATTLE_GROUND_GAME_OBJECT_NAME = "Battle Ground";
    private const string OBSTACLES_GAME_OBJECT_NAME = "Obstacles";

    [SerializeField]
    private int _tileMapRows = 16;
    [SerializeField]
    private int _tileMapColumns = 16;

    [SerializeField]
    private float _timeBetweenSpawningTiles = 0.2f;

    [SerializeField]
    private Vector3Int _tileMapOffset = new Vector3Int(-5, 4, 0);
    [SerializeField]
    private Vector2 _environmentHazardOffset = new Vector2(0.5f, 0.4f);
    [SerializeField]
    private float _reservedPositionOffset = 2f;

    [SerializeField]
    private D_Tiles _tilesData;

    [SerializeField]
    private bool _shouldGenerateEnvironmentHazards = true;
    [SerializeField]
    private GameObject _environmentHazardsContainer;

    private Vector2[] _reservedPositions;

    private Tilemap _outerWalls;
    private Tilemap _battleGround;
    private Tilemap _obstacles;

    public Action LevelGeneratedEvent;

    private GameObject _lastPortalGameObject;
    private int _numberOfPortals;

    private void Awake()
    {
        _outerWalls = transform.Find(OUTER_WALLS_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
        _battleGround = transform.Find(BATTLE_GROUND_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
        _obstacles = transform.Find(OBSTACLES_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
    }

    private void Start()
    {
        _reservedPositions = FindObjectOfType<PlayerManager>().PlayersPositions;

        StartCoroutine(GenerateLevel());
    }

    private IEnumerator GenerateLevel()
    {
        Vector3Int position = Vector3Int.zero;
        Vector2 positionToCheck = Vector2.zero;
        int wallsIndex = 0;
        int leftSideWallsIndex = 0;
        int rightSideWallsIndex = 0;

        for (int column = 0; column < _tileMapColumns; column++)
        {
            for (int row = 0; row < _tileMapRows; row++)
            {
                yield return new WaitForSeconds(_timeBetweenSpawningTiles);
                position.Set(column + _tileMapOffset.x, -row + _tileMapOffset.y, 0);

                if (IsFirstRow(row) && IsFirstColumn(column))
                {
                    _outerWalls.SetTile(position, _tilesData.upperLeftCorner);
                }
                else if (IsFirstRow(row) && IsLastColumn(column))
                {
                    _outerWalls.SetTile(position, _tilesData.upperRightCorner);
                }
                else if (IsLastRow(row) && IsFirstColumn(column))
                {
                    _outerWalls.SetTile(position, _tilesData.lowerLeftCorner);
                }
                else if (IsLastRow(row) && IsLastColumn(column))
                {
                    _outerWalls.SetTile(position, _tilesData.lowerRightCorner);
                }
                else if (IsBetweenCorners(column))
                {
                    if (IsFirstRow(row))
                    {
                        _outerWalls.SetTile(position, _tilesData.upperWalls[wallsIndex]);
                        wallsIndex++;
                    }
                    else if (IsLastRow(row))
                    {
                        _outerWalls.SetTile(position, _tilesData.lowerWalls[wallsIndex]);
                        wallsIndex++;
                    }
                    else
                    {
                        _battleGround.SetTile(position, GetRandomTile(_tilesData.floors));

                        GenerateObstacle(position, positionToCheck);
                    }
                }
                else
                {
                    if (IsFirstColumn(column))
                    {
                        _outerWalls.SetTile(position, _tilesData.leftWalls[leftSideWallsIndex]);
                        leftSideWallsIndex++;
                    }
                    if (IsLastColumn(column))
                    {
                        _outerWalls.SetTile(position, _tilesData.rightWalls[rightSideWallsIndex]);
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

        if (_numberOfPortals == 1)
        {
            Destroy(_lastPortalGameObject);
        }

        LevelGeneratedEvent?.Invoke();
    }

    private void GenerateObstacle(Vector3Int position, Vector2 positionToCheck)
    {
        if (!_shouldGenerateEnvironmentHazards)
        {
            return;
        }

        int randomObstacle = UnityEngine.Random.Range(0, 100);

        positionToCheck.Set(position.x, position.y);

        if (!IsReservedPosition(position))
        {
            if (randomObstacle < _tilesData.chanceForEnvironmentHazards)
            {
                randomObstacle = UnityEngine.Random.Range(0, 100);

                positionToCheck += _environmentHazardOffset;

                GameObject environmentHazard = null;

                if (randomObstacle < _tilesData.chanceForEnvironmentHazards)
                {
                    environmentHazard = Instantiate(GetRandomEnvironmentHazard(_tilesData.environmentHazards), positionToCheck, Quaternion.identity);
                }
                else if (randomObstacle < _tilesData.chanceForEnvironmentHazardsWithRandomRotation)
                {
                    environmentHazard = Instantiate(GetRandomEnvironmentHazard(_tilesData.environmentHazardsWithRandomRotation), positionToCheck, GetRandomRotation());
                }
                else if (randomObstacle < _tilesData.chanceForPortals)
                {
                    environmentHazard = Instantiate(_tilesData.portal, positionToCheck, Quaternion.identity);
                    _numberOfPortals++;

                    if (_numberOfPortals == 2)
                    {
                        Portal newPortal = environmentHazard.GetComponent<Portal>();
                        Portal lastPortal = _lastPortalGameObject.GetComponent<Portal>();
                        lastPortal.SetConnectedPortal(newPortal);
                        newPortal.SetConnectedPortal(lastPortal);
                        _numberOfPortals = 0;
                    }
                    else
                    {
                        _lastPortalGameObject = environmentHazard;
                    }
                }
                if (environmentHazard)
                {
                    environmentHazard.transform.parent = _environmentHazardsContainer.gameObject.transform;
                }
            }
            else if (randomObstacle < _tilesData.chanceForObstacle)
            {
                _obstacles.SetTile(position, GetRandomTile(_tilesData.obstacles));
            }
        }
    }

    private bool IsBetweenCorners(int column) => column > 0 && column < _tileMapColumns - 1;

    private bool IsFirstColumn(int column) => column == 0;

    private bool IsLastColumn(int column) => column == _tileMapColumns - 1;

    private bool IsFirstRow(int row) => row == 0;

    private bool IsLastRow(int row) => row == _tileMapRows - 1;

    private bool IsReservedPosition(Vector3Int reservedPosition) => _reservedPositions.Any(position =>
    position.x + _reservedPositionOffset > reservedPosition.x && position.x - _reservedPositionOffset < reservedPosition.x
    && position.y + _reservedPositionOffset > reservedPosition.y && position.y - _reservedPositionOffset < reservedPosition.y
    );

    private Tile GetRandomTile(Tile[] tiles) => tiles[UnityEngine.Random.Range(0, tiles.Length)];

    private GameObject GetRandomEnvironmentHazard(GameObject[] environmentHazards) => environmentHazards[UnityEngine.Random.Range(0,
        _tilesData.environmentHazards.Length)];

    private Quaternion GetRandomRotation() => Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 4) * 90);
}
