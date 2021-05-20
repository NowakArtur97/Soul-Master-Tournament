using UnityEngine;

public class PortalGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private int _tileMapRows;
    private int _tileMapColumns;
    private Vector2 _environmentHazardOffset;
    private Vector3 _tileMapOffset;
    private LayerMask[] _hazardsMasks;
    private GameObject _environmentHazardsContainer;
    private Vector2[] _reservedPositions;
    private float _reservedPositionOffset;

    public PortalGeneratorStrategy(int tileMapRows, int tileMapColumns, Vector2 environmentHazardOffset, Vector3Int tileMapOffset, LayerMask[] hazardsMasks,
        GameObject environmentHazardsContainer, Vector2[] reservedPositions, float reservedPositionOffset)
    {
        _tileMapRows = tileMapRows - 2;
        _tileMapColumns = tileMapColumns - 2;
        _environmentHazardOffset = environmentHazardOffset;
        _tileMapOffset = tileMapOffset;
        _hazardsMasks = hazardsMasks;
        _environmentHazardsContainer = environmentHazardsContainer;
        _reservedPositions = reservedPositions;
        _reservedPositionOffset = reservedPositionOffset;
    }

    public GameObject generate(GameObject environmentHazard, Vector2 obstaclePosition)
    {

        Vector2 randomPositionOfSecondPortal = new Vector2(Random.Range(1, _tileMapColumns), -Random.Range(1, _tileMapRows)) + (Vector2)_tileMapOffset;

        if (!GeneratorUtil.IsAlreadyTaken(randomPositionOfSecondPortal, _hazardsMasks)
                    && GeneratorUtil.IsPositionFree(new Vector3Int((int)randomPositionOfSecondPortal.x, (int)randomPositionOfSecondPortal.y, 0),
                        _reservedPositions, _reservedPositionOffset))
        {
            GameObject portal1GameObject = MonoBehaviour.Instantiate(environmentHazard, obstaclePosition, Quaternion.identity);

            GameObject portal2GameObject = MonoBehaviour.Instantiate(environmentHazard, randomPositionOfSecondPortal + _environmentHazardOffset, Quaternion.identity);
            _environmentHazardsContainer.transform.parent = _environmentHazardsContainer.transform;

            Portal portal1 = portal1GameObject.GetComponent<Portal>();
            Portal portal2 = portal2GameObject.GetComponent<Portal>();

            portal1.SetConnectedPortal(portal2);
            portal2.SetConnectedPortal(portal1);

            return portal1GameObject;
        }

        return null;
    }
}
