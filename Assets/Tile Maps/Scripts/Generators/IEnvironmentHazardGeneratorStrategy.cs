using UnityEngine;

public interface IEnvironmentHazardGeneratorStrategy
{
    GameObject generate(GameObject environmentHazard, Vector2 obstaclePosition);
}
