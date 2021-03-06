using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    private const string OUTER_WALLS_GAME_OBJECT_NAME = "Outer Walls";
    private const string BATTLE_GROUND_GAME_OBJECT_NAME = "Battle Ground";

    [SerializeField]
    private int _tileMapRows = 18;
    [SerializeField]
    private int _tileMapColumns = 18;
    [SerializeField]
    private D_Tiles _tilesData;
    [SerializeField]
    private float _timeBeforeSpawningTiles = 0.001f;
    [SerializeField]
    private Vector2 _tileMapOffset = new Vector2(-5, 4);

    private Tilemap _outerWalls;
    private Tilemap _battleGround;

    private void Awake()
    {
        _outerWalls = transform.Find(OUTER_WALLS_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
        _battleGround = transform.Find(BATTLE_GROUND_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
    }

    private void Start() => StartCoroutine(GenerateLevel());

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
                yield return new WaitForSeconds(_timeBeforeSpawningTiles);
                position.Set((int)(column + _tileMapOffset.x), (int)(-row + _tileMapOffset.y), 0);

                if (GeneratorUtil.IsFirstRow(row) && GeneratorUtil.IsFirstColumn(column))
                {
                    _outerWalls.SetTile(position, _tilesData.upperLeftCorner);
                }
                else if (GeneratorUtil.IsFirstRow(row) && GeneratorUtil.IsLastColumn(column, _tileMapColumns))
                {
                    _outerWalls.SetTile(position, _tilesData.upperRightCorner);
                }
                else if (GeneratorUtil.IsLastRow(row, _tileMapRows) && GeneratorUtil.IsFirstColumn(column))
                {
                    _outerWalls.SetTile(position, _tilesData.lowerLeftCorner);
                }
                else if (GeneratorUtil.IsLastRow(row, _tileMapRows) && GeneratorUtil.IsLastColumn(column, _tileMapColumns))
                {
                    _outerWalls.SetTile(position, _tilesData.lowerRightCorner);
                }
                else if (GeneratorUtil.IsBetweenCorners(column, _tileMapColumns))
                {
                    if (GeneratorUtil.IsFirstRow(row))
                    {
                        _outerWalls.SetTile(position, _tilesData.upperWalls[wallsIndex]);
                        wallsIndex++;
                    }
                    else if (GeneratorUtil.IsLastRow(row, _tileMapRows))
                    {
                        _outerWalls.SetTile(position, _tilesData.lowerWalls[wallsIndex]);
                        wallsIndex++;
                    }
                    else
                    {
                        _battleGround.SetTile(position, GetRandomTile(_tilesData.floors));
                    }
                }
                else
                {
                    if (GeneratorUtil.IsFirstColumn(column))
                    {
                        _outerWalls.SetTile(position, _tilesData.leftWalls[leftSideWallsIndex]);
                        leftSideWallsIndex++;
                    }
                    if (GeneratorUtil.IsLastColumn(column, _tileMapColumns))
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
    }

    private Tile GetRandomTile(Tile[] tiles) => tiles[Random.Range(0, tiles.Length)];
}
