using UnityEngine;

public interface IEnvironmentHazardGeneratorStrategy
{
    GameObject Generate(D_EnvironmentHazard randomHazardData, Vector2 obstaclePosition);
}
