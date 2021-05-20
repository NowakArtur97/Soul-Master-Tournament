using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class EnvironmentHazardGenerator : MonoBehaviour
{
    private const string OBSTACLES_GAME_OBJECT_NAME = "Obstacles";

    [Header("Level Data")]
    [SerializeField]
    private int _tileMapRows = 18;
    [SerializeField]
    private int _tileMapColumns = 18;
    [SerializeField]
    private float _timeBeforeSpawningEnvironmentHazards = 2f;
    [SerializeField]
    private float _timeBetweenSpawningEnvironmentHazards = 0.001f;
    [SerializeField]
    private Vector3Int _tileMapOffset = new Vector3Int(-5, 4, 0);
    [SerializeField]
    private GameObject _environmentHazardsContainer;
    [SerializeField]
    private float _reservedPositionOffset = 1.5f;

    [Header("Environment Hazards")]
    [SerializeField]
    private Vector2 _environmentHazardOffset = new Vector2(0.5f, 0.55f);
    [SerializeField]
    private D_EnvironmentHazard[] _environmentHazardsData;

    private Tilemap _obstacles;
    private Vector2[] _reservedPositions;
    public Action LevelGeneratedEvent;

    private IEnvironmentHazardGeneratorStrategy _generatorStrategy;

    private void Awake()
    {
        _obstacles = transform.Find(OBSTACLES_GAME_OBJECT_NAME).gameObject.GetComponent<Tilemap>();
    }

    private void Start()
    {
        _reservedPositions = FindObjectOfType<PlayerManager>().PlayersPositions;

        StartCoroutine(GenerateEnvironmentHazards());
    }

    private IEnumerator GenerateEnvironmentHazards()
    {
        yield return new WaitForSeconds(_timeBeforeSpawningEnvironmentHazards);

        Vector3Int position = Vector3Int.zero;
        Vector2 obstaclePosition = Vector2.zero;

        for (int column = 0; column < _tileMapColumns; column++)
        {
            for (int row = 0; row < _tileMapRows; row++)
            {
                yield return new WaitForSeconds(_timeBetweenSpawningEnvironmentHazards);
                position.Set(column + _tileMapOffset.x, -row + _tileMapOffset.y, 0);

                if (GeneratorUtil.IsFreePosition(position, _reservedPositions, _reservedPositionOffset))
                {
                    int chanceForHazard = UnityEngine.Random.Range(0, 100);
                    obstaclePosition.Set(position.x, position.y);
                    obstaclePosition += _environmentHazardOffset;

                    GameObject hazard = null;
                    Quaternion rotation;
                    D_EnvironmentHazard randomHazardData = _environmentHazardsData.FirstOrDefault(data => data != null
                        && chanceForHazard <= data.chanceForEnvironmentHazard);

                    if (randomHazardData)
                    {
                        if ((GeneratorUtil.IsOnWall(column, _tileMapColumns, row, _tileMapRows) && randomHazardData.isOnWall)
                            || !GeneratorUtil.IsOnWall(column, _tileMapColumns, row, _tileMapRows) && !randomHazardData.isOnWall)
                        {
                            GameObject hazardPrefab = randomHazardData.environmentHazard;

                            rotation = GetRotation(hazardPrefab);
                            hazard = Instantiate(hazardPrefab, obstaclePosition, rotation);

                            hazard.transform.parent = _environmentHazardsContainer.gameObject.transform;
                        }
                    }
                }
            }
        }

        LevelGeneratedEvent?.Invoke();
    }

    private Quaternion GetRotation(GameObject hazardPrefab)
    {
        EnvironmentHazard hazard = hazardPrefab.GetComponent<EnvironmentHazard>();

        //if (hazard is Spikes)
        //{
        //    return Quaternion.identity;
        //}
        //else
        {
            return Quaternion.identity;
        }
    }
}
