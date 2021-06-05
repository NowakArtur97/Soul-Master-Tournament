using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EnvironmentHazardGenerator : MonoBehaviour
{
    private const string OBSTACLES_GAME_OBJECT_NAME = "Obstacles";
    private const string PORTAL_GAME_OBJECT_NAME = "Portal";
    private const string BALLISTA_GAME_OBJECT_NAME = "Ballista";
    private const string FLAMETHROWER_GAME_OBJECT_NAME = "Flemethrower";
    private const string POISON_ARROW_LAUNCHER_GAME_OBJECT_NAME = "Poison Arrow Launcher";
    private const string SLIDING_SAW_GAME_OBJECT_NAME = "Sliding Saw";

    [Header("Level Data")]
    [SerializeField]
    private bool _shouldGenerateHazards = true;
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
    private LayerMask[] _hazardsMasks;
    [SerializeField]
    private LayerMask _wallLayerMask;
    [SerializeField]
    private D_EnvironmentHazard[] _environmentHazardsData;
    [SerializeField]
    private GameObject[] _rails;

    private List<Vector2> _reservedPositions = new List<Vector2>();
    public Action LevelGeneratedEvent;
    private List<IEnvironmentHazardGeneratorStrategy> _generators;
    private System.Random _random = new System.Random();

    private void Start()
    {
        _reservedPositions.AddRange(FindObjectOfType<PlayerManager>().PlayersPositions);

        _generators = new List<IEnvironmentHazardGeneratorStrategy>();

        StartCoroutine(GenerateEnvironmentHazards());
    }

    private IEnumerator GenerateEnvironmentHazards()
    {
        yield return new WaitForSeconds(_timeBeforeSpawningEnvironmentHazards);

        Vector3Int position = Vector3Int.zero;
        Vector2 obstaclePosition = Vector2.zero;

        if (_shouldGenerateHazards)
        {
            for (int column = 0; column < _tileMapColumns; column++)
            {
                for (int row = 0; row < _tileMapRows; row++)
                {
                    yield return new WaitForSeconds(_timeBetweenSpawningEnvironmentHazards);

                    if (GeneratorUtil.IsInCorner(column, _tileMapColumns, row, _tileMapRows))
                    {
                        continue;
                    }

                    position.Set(column + _tileMapOffset.x, -row + _tileMapOffset.y, 0);

                    if (GeneratorUtil.IsPositionFree(position, _reservedPositions, _reservedPositionOffset))
                    {
                        int chanceForHazard = UnityEngine.Random.Range(0, 100);
                        obstaclePosition.Set(position.x, position.y);

                        GameObject hazard = null;
                        _environmentHazardsData = _environmentHazardsData.OrderBy(a => _random.Next()).ToArray(); // shuffle array
                        D_EnvironmentHazard randomHazardData = _environmentHazardsData.FirstOrDefault(data => data != null
                            && chanceForHazard <= data.chanceForEnvironmentHazard);

                        if (randomHazardData)
                        {
                            if ((GeneratorUtil.IsOnWall(column, _tileMapColumns, row, _tileMapRows) && randomHazardData.isOnWall)
                                                 || (!GeneratorUtil.IsOnWall(column, _tileMapColumns, row, _tileMapRows) && !randomHazardData.isOnWall))
                            {
                                GameObject environmentHazard = randomHazardData.environmentHazard;

                                hazard = ChoseGenerationStrategy(environmentHazard, row, column).Generate(randomHazardData, obstaclePosition);

                                if (hazard)
                                {
                                    _reservedPositions.Add(hazard.transform.position);
                                    hazard.transform.position += (Vector3)randomHazardData.environmentHazardOffset;
                                    hazard.transform.parent = _environmentHazardsContainer.gameObject.transform;
                                }
                            }
                        }
                    }
                }
            }
        }
        LevelGeneratedEvent?.Invoke();
    }

    private IEnvironmentHazardGeneratorStrategy ChoseGenerationStrategy(GameObject environmentHazard, int row, int column)
    {
        IEnvironmentHazardGeneratorStrategy generatorStrategy = null;

        if (Is(environmentHazard, PORTAL_GAME_OBJECT_NAME))
        {
            generatorStrategy = _generators.OfType<PortalGeneratorStrategy>().FirstOrDefault();
            if (generatorStrategy == null)
            {
                generatorStrategy = new PortalGeneratorStrategy(_tileMapRows, _tileMapColumns, _tileMapOffset, _hazardsMasks, _environmentHazardsContainer,
                    _reservedPositions, _reservedPositionOffset);
                _generators.Add(generatorStrategy);
            }
        }
        else if (Is(environmentHazard, POISON_ARROW_LAUNCHER_GAME_OBJECT_NAME) || Is(environmentHazard, FLAMETHROWER_GAME_OBJECT_NAME))
        {
            generatorStrategy = _generators.OfType<HazardOnWallGeneratorStrategy>().FirstOrDefault();
            if (generatorStrategy == null)
            {
                generatorStrategy = new HazardOnWallGeneratorStrategy(_tileMapRows, _tileMapColumns, _environmentHazardsContainer, _tileMapOffset);
                _generators.Add(generatorStrategy);
            }
        }
        else if (Is(environmentHazard, BALLISTA_GAME_OBJECT_NAME))
        {
            generatorStrategy = _generators.OfType<OppositeToWallGeneratorStrategy>().FirstOrDefault();
            if (generatorStrategy == null)
            {
                generatorStrategy = new OppositeToWallGeneratorStrategy(_wallLayerMask);
                _generators.Add(generatorStrategy);
            }
        }
        else if (Is(environmentHazard, SLIDING_SAW_GAME_OBJECT_NAME))
        {
            generatorStrategy = _generators.OfType<RailsGeneratorStrategy>().FirstOrDefault();
            bool wasNull = generatorStrategy == null;
            generatorStrategy = new RailsGeneratorStrategy(_tileMapRows, _tileMapColumns, _rails, _environmentHazardsContainer, _reservedPositions,
               _reservedPositionOffset, row, column);
            if (wasNull)
            {
                _generators.Add(generatorStrategy);
            }
        }
        else
        {
            generatorStrategy = _generators.OfType<DefaultGeneratorStrategy>().FirstOrDefault();
            if (generatorStrategy == null)
            {
                generatorStrategy = new DefaultGeneratorStrategy();
                _generators.Add(generatorStrategy);
            }
        }

        return generatorStrategy;
    }

    private static bool Is(GameObject environmentHazard, string name) => environmentHazard.name.StartsWith(name);
}
