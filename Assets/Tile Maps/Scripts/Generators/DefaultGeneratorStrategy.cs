using UnityEngine;

public class DefaultGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    public GameObject generate(GameObject environmentHazard, Vector2 obstaclePosition) =>
        MonoBehaviour.Instantiate(environmentHazard, obstaclePosition, Quaternion.identity);
}
