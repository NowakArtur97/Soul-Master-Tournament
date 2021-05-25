using UnityEngine;

public class OppositeToWallGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    private Vector2[] _directionsToCheck;
    private LayerMask _wallLayerMask;

    public OppositeToWallGeneratorStrategy(LayerMask wallLayerMask)
    {
        _directionsToCheck = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
        _wallLayerMask = wallLayerMask;
    }

    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition) =>
        MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, GetDirectionFurthestFromWall(obstaclePosition));

    private Quaternion GetDirectionFurthestFromWall(Vector2 obstaclePosition)
    {
        Vector2 directionFurthestFromWall = _directionsToCheck[0];
        float distanceFurthestFromWall = 0;

        for (int index = 1; index < _directionsToCheck.Length; index++)
        {
            Vector2 direction = _directionsToCheck[index];
            RaycastHit2D wallHit = Physics2D.Raycast(obstaclePosition, direction, 50, _wallLayerMask);

            if (wallHit)
            {
                float wallHitDistance = wallHit.distance;

                if (wallHitDistance >= distanceFurthestFromWall)
                {
                    distanceFurthestFromWall = wallHitDistance;
                    directionFurthestFromWall = direction;
                }
            }
        }
        Debug.Log(directionFurthestFromWall);
        Debug.Log(distanceFurthestFromWall);
        return Quaternion.Euler(directionFurthestFromWall.x, directionFurthestFromWall.y, 0f);
    }
}
