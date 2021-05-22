using System;
using UnityEngine;

public class HazardOnWallGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private int _tileMapRows;
    private int _tileMapColumns;
    private GameObject _environmentHazardsContainer;
    private Vector3Int _tileMapOffset;

    public HazardOnWallGeneratorStrategy(int tileMapRows, int tileMapColumns, GameObject environmentHazardsContainer, Vector3Int tileMapOffset)
    {
        _tileMapRows = tileMapRows;
        _tileMapColumns = tileMapColumns;
        _environmentHazardsContainer = environmentHazardsContainer;
        _tileMapOffset = tileMapOffset;
    }

    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition) =>
        MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, GetRotation(obstaclePosition));

    private Quaternion GetRotation(Vector2 obstaclePosition)
    {
        if (GeneratorUtil.IsFirstRow((int)(obstaclePosition.y - _tileMapOffset.y)))
        {
            return Quaternion.Euler(0, 0, 0);
        }
        else if (GeneratorUtil.IsLastRow(-(int)(obstaclePosition.y - _tileMapOffset.y), _tileMapColumns))
        {
            return Quaternion.Euler(0, 0, -180);
        }
        else if (GeneratorUtil.IsFirstColumn((int)(obstaclePosition.x - _tileMapOffset.x)))
        {
            return Quaternion.Euler(0, 0, 90);
        }
        else
        {
            return Quaternion.Euler(0, 0, -90);
        }
    }
}
