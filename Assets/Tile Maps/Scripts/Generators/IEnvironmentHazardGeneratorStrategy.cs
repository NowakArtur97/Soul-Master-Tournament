using UnityEngine;

public interface IEnvironmentHazardGeneratorStrategy
{
    GameObject Generate(D_EnvironmentHazard environmentHazardData, Vector2 obstaclePosition);
}
