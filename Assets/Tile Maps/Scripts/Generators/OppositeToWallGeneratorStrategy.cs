using UnityEngine;

public class OppositeToWallGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private const int DISTANCE_TO_CHECK = 30;

    private Vector2[] _directionsToCheck;
    private LayerMask _wallLayerMask;

    public OppositeToWallGeneratorStrategy(LayerMask wallLayerMask)
    {
        _directionsToCheck = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
        _wallLayerMask = wallLayerMask;
    }

    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition)
    {
        GameObject hazard = MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, Quaternion.identity);

        TurnTowardsFarthestWall(hazard);

        return hazard;
    }

    private void TurnTowardsFarthestWall(GameObject hazard)
    {
        Vector2 directionFurthestFromWall = _directionsToCheck[0];
        float distanceFurthestFromWall = Physics2D.Raycast(hazard.transform.position, directionFurthestFromWall, DISTANCE_TO_CHECK, _wallLayerMask).distance;

        for (int index = 1; index < _directionsToCheck.Length; index++)
        {
            Vector2 direction = _directionsToCheck[index];
            float wallHitDistance = Physics2D.Raycast(hazard.transform.position, direction, DISTANCE_TO_CHECK, _wallLayerMask).distance;

            if (wallHitDistance >= distanceFurthestFromWall)
            {
                distanceFurthestFromWall = wallHitDistance;
                directionFurthestFromWall = direction;
            }
        }

        hazard.transform.right = directionFurthestFromWall;
    }
}
