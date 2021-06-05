using System.Collections.Generic;
using UnityEngine;

public class RailsGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private int _row;
    private int _column;
    private int _tileMapRows;
    private int _tileMapColumns;
    private Vector3 _tileMapOffset;
    private GameObject[] _rails;
    private GameObject _environmentHazardsContainer;
    private List<Vector2> _reservedPositions;
    private float _reservedPositionOffset;

    private Vector2[] _directions;

    public RailsGeneratorStrategy(int tileMapRows, int tileMapColumns, GameObject[] rails, GameObject environmentHazardsContainer, List<Vector2> reservedPositions,
        float reservedPositionOffset, int row, int column)
    {
        _tileMapRows = tileMapRows;
        _tileMapColumns = tileMapColumns;
        _rails = rails;
        _environmentHazardsContainer = environmentHazardsContainer;
        _reservedPositions = reservedPositions;
        _reservedPositionOffset = reservedPositionOffset;
        _directions = new Vector2[] { Vector2.up, Vector2.left, Vector2.right, Vector2.down };
        _row = row;
        _column = column;
    }

    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition)
    {
        Vector2 direction = _directions[Random.Range(0, _directions.Length - 1)];

        Vector2 leftSideRailPosition = new Vector2(obstaclePosition.x, obstaclePosition.y) - direction;
        Vector2 middleSideRailPosition = new Vector2(obstaclePosition.x, obstaclePosition.y);
        Vector2 rightSideRailPosition = new Vector2(obstaclePosition.x, obstaclePosition.y) + direction;

        if (IsFree(leftSideRailPosition) && !GeneratorUtil.IsOnWall(_row - 1, _tileMapColumns, _column, _tileMapRows)
            && !GeneratorUtil.IsOnWall(_column - 1, _tileMapColumns, _row, _tileMapRows)
            && IsFree(middleSideRailPosition)
              && IsFree(rightSideRailPosition) && !GeneratorUtil.IsOnWall(_row + 1, _tileMapColumns, _column, _tileMapRows)
            && !GeneratorUtil.IsOnWall(_column + 1, _tileMapColumns, _row, _tileMapRows))
        {
            GameObject slidingSawGameObject = MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, Quaternion.identity);

            SpawnRail(_rails[0], leftSideRailPosition, environmentHazardData, direction);
            GameObject middleSideRail = SpawnRail(_rails[1], middleSideRailPosition, environmentHazardData, direction);
            SpawnRail(_rails[2], rightSideRailPosition, environmentHazardData, direction);

            slidingSawGameObject.transform.position = middleSideRail.transform.position - (Vector3)environmentHazardData.environmentHazardOffset;
            slidingSawGameObject.transform.right = direction;

            return slidingSawGameObject;
        }

        return null;
    }

    private GameObject SpawnRail(GameObject rail, Vector2 position, D_EnvironmentHazard environmentHazardData, Vector2 direction)
    {
        GameObject railGameObject = MonoBehaviour.Instantiate(rail, position + environmentHazardData.environmentHazardOffset, Quaternion.identity);

        railGameObject.transform.parent = _environmentHazardsContainer.transform;
        _reservedPositions.Add(railGameObject.transform.position);
        railGameObject.transform.right = direction;

        return railGameObject;
    }

    private bool IsFree(Vector2 railPosition) =>
        !_reservedPositions.Contains(railPosition)
                           && GeneratorUtil.IsPositionFree(new Vector3Int((int)railPosition.x, (int)railPosition.y, 0),
                               _reservedPositions, _reservedPositionOffset);
}
