using System.Collections;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _minPosition;
    [SerializeField]
    private Vector2 _maxPosition;
    [SerializeField]
    private GameObject[] _pickUps;
    [SerializeField]
    private float _minTimeBetweenSpawns = 10;
    [SerializeField]
    private float _maxTimeBetweenSpawns = 30;
    [SerializeField]
    private float _areaToCheck = 0.8f;
    [SerializeField]
    private LayerMask _pickUpLayer;

    private Coroutine _spawnCoroutine;
    private bool _isSpawning;
    private bool _isLevelGenerated;
    private Vector2 _areaVectorToCheck;

    private EnvironmentHazardGenerator _environmentHazardGenerator;

    private void Start()
    {
        _environmentHazardGenerator = FindObjectOfType<EnvironmentHazardGenerator>();

        _environmentHazardGenerator.LevelGeneratedEvent += OnLevelGenerated;

        _isSpawning = false;
        _isLevelGenerated = false;

        _areaVectorToCheck = new Vector2(_areaToCheck, _areaToCheck);
    }

    private void Update()
    {
        if (!_isSpawning && _isLevelGenerated)
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        _isSpawning = true;

        yield return new WaitForSeconds(ChoseRandomTimeBetweenSpawns());

        SpawnPickUp(ChoseRandomLocation());

        _isSpawning = false;
    }

    public void SpawnPickUp(Vector2 position)
    {
        if (IsLocationFree(position))
        {
            GameObject pickUp = Instantiate(ChoseRandomPickUp(), position, Quaternion.identity);
            pickUp.transform.parent = gameObject.transform;
        }
        else
        {
            SpawnPickUp(ChoseRandomLocation());
        }
    }

    private void OnLevelGenerated()
    {
        _environmentHazardGenerator.LevelGeneratedEvent -= OnLevelGenerated;

        _isLevelGenerated = true;
    }

    private GameObject ChoseRandomPickUp() => _pickUps[Random.Range(0, _pickUps.Length)];

    private Vector2 ChoseRandomLocation() => new Vector2((int)Random.Range(_minPosition.x, _maxPosition.x), (int)Random.Range(_minPosition.y, _maxPosition.y));

    private float ChoseRandomTimeBetweenSpawns() => Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);

    private bool IsLocationFree(Vector2 position) => !Physics2D.BoxCast(position, _areaVectorToCheck, 0, Vector2.up, _areaToCheck, _pickUpLayer);
}
