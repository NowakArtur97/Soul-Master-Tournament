using UnityEngine;

public class DefaultGeneratorStrategy : IEnvironmentHazardGeneratorStrategy
{
    public GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition) =>
        MonoBehaviour.Instantiate(environmentHazardData.environmentHazard, obstaclePosition, Quaternion.identity);
}
