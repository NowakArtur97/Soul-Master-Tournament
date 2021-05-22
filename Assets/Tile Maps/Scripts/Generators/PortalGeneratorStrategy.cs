using System.Collections.Generic;
using UnityEngine;

public class PortalGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private int _tileMapRows;
    private int _tileMapColumns;
    private Vector3 _tileMapOffset;
    private LayerMask[] _hazardsMasks;
    private GameObject _environmentHazardsContainer;
    private List<Vector2> _reservedPositions;
    private float _reservedPositionOffset;

    public PortalGeneratorStrategy(int tileMapRows, int tileMapColumns, Vector3Int tileMapOffset, LayerMask[] hazardsMasks,
        GameObject environmentHazardsContainer, List<Vector2> reservedPositions, float reservedPositionOffset)
    {
        _tileMapRows = tileMapRows - 2;
        _tileMapColumns = tileMapColumns - 2;
        _tileMapOffset = tileMapOffset;
        _hazardsMasks = hazardsMasks;
        _environmentHazardsContainer = environmentHazardsContainer;
        _reservedPositions = reservedPositions;
        _reservedPositionOffset = reservedPositionOffset;
    }

    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition)
    {
        Vector2 randomPositionOfSecondPortal = new Vector2(Random.Range(1, _tileMapColumns), -Random.Range(1, _tileMapRows)) + (Vector2)_tileMapOffset;

        if (!_reservedPositions.Contains(randomPositionOfSecondPortal)
                    && GeneratorUtil.IsPositionFree(new Vector3Int((int)randomPositionOfSecondPortal.x, (int)randomPositionOfSecondPortal.y, 0),
                        _reservedPositions, _reservedPositionOffset))
        {
            GameObject hazardPrefab = environmentHazardData.environmentHazard;
            GameObject portal1GameObject = MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, Quaternion.identity);

            GameObject portal2GameObject = MonoBehaviour.Instantiate(hazardPrefab, randomPositionOfSecondPortal + environmentHazardData.environmentHazardOffset,
                Quaternion.identity);
            portal2GameObject.transform.parent = _environmentHazardsContainer.transform;
            _reservedPositions.Add(portal2GameObject.transform.position);

            Portal portal1 = portal1GameObject.GetComponent<Portal>();
            Portal portal2 = portal2GameObject.GetComponent<Portal>();

            portal1.SetConnectedPortal(portal2);
            portal2.SetConnectedPortal(portal1);

            return portal1GameObject;
        }

        return null;
    }
}
