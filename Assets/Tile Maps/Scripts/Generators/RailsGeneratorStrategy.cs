using System.Collections.Generic;
using UnityEngine;

public class RailsGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private int _tileMapRows;
    private int _tileMapColumns;
    private Vector3 _tileMapOffset;
    private GameObject[] _rails;
    private GameObject _environmentHazardsContainer;
    private List<Vector2> _reservedPositions;
    private float _reservedPositionOffset;

    public RailsGeneratorStrategy(int tileMapRows, int tileMapColumns, GameObject[] rails, GameObject environmentHazardsContainer, List<Vector2> reservedPositions,
        float reservedPositionOffset)
    {
        _tileMapRows = tileMapRows;
        _tileMapColumns = tileMapColumns;
        _rails = rails;
        _environmentHazardsContainer = environmentHazardsContainer;
        _reservedPositions = reservedPositions;
        _reservedPositionOffset = reservedPositionOffset;
    }

    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition)
    {
        Vector2 leftSideRailPosition = new Vector2(obstaclePosition.x - 1, obstaclePosition.y);
        Vector2 middleSideRailPosition = new Vector2(obstaclePosition.x, obstaclePosition.y);
        Vector2 rightSideRailPosition = new Vector2(obstaclePosition.x + 1, obstaclePosition.y);

        if (IsFree(leftSideRailPosition)
         && IsFree(middleSideRailPosition)
           && IsFree(rightSideRailPosition))
        {
            GameObject slidingSawGameObject = MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, Quaternion.identity);

            GameObject leftSideRail = MonoBehaviour.Instantiate(_rails[0], leftSideRailPosition + environmentHazardData.environmentHazardOffset,
                Quaternion.identity);

            leftSideRail.transform.parent = _environmentHazardsContainer.transform;
            _reservedPositions.Add(leftSideRail.transform.position);

            GameObject middleSideRail = MonoBehaviour.Instantiate(_rails[1], middleSideRailPosition + environmentHazardData.environmentHazardOffset,
                Quaternion.identity);

            middleSideRail.transform.parent = _environmentHazardsContainer.transform;
            _reservedPositions.Add(middleSideRail.transform.position);

            GameObject rightSideRail = MonoBehaviour.Instantiate(_rails[2], rightSideRailPosition + environmentHazardData.environmentHazardOffset,
                Quaternion.identity);

            leftSideRail.transform.parent = _environmentHazardsContainer.transform;
            _reservedPositions.Add(leftSideRail.transform.position);

            slidingSawGameObject.transform.position = middleSideRail.transform.position - (Vector3)environmentHazardData.environmentHazardOffset;

            return slidingSawGameObject;
        }

        return null;
    }

    private bool IsFree(Vector2 railPosition) =>
        !_reservedPositions.Contains(railPosition)
                           && GeneratorUtil.IsPositionFree(new Vector3Int((int)railPosition.x, (int)railPosition.y, 0),
                               _reservedPositions, _reservedPositionOffset);
}
