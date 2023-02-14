using System.Collections;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _minPosition = new Vector2(-5, 4);
    [SerializeField]
    private Vector2 _maxPosition = new Vector2(12, -13);
    [SerializeField]
    private GameObject[] _pickUps;
    [SerializeField]
    private float _minTimeBetweenSpawns = 15;
    [SerializeField]
    private float _maxTimeBetweenSpawns = 20;
    [SerializeField]
    private Vector2 _raycastOffset = new Vector2(0.1f, 0.5f);
    [SerializeField]
    private float _distanceToCheck = 0.8f;
    [SerializeField]
    private LayerMask _layersToIgnore;
    [SerializeField]
    private int _maxSpawnAttempts = 3;

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
        Vector2 position = FindPosition();

        if (position != null)
        {
            SpawnPickUp(position);
        }

        _isSpawning = false;
    }

    private Vector2 FindPosition()
    {
        Vector2 position = ChoseRandomLocation();

        int attempt = 0;

        while (IsLocationOccupied(position) && attempt < _maxSpawnAttempts)
        {
            position = ChoseRandomLocation();
            attempt++;
        }

        return position;
    }

    public void SpawnPickUp(Vector2 position) => Instantiate(ChoseRandomPickUp(), position, Quaternion.identity);

    private void OnLevelGenerated()
    {
        _environmentHazardGenerator.LevelGeneratedEvent -= OnLevelGenerated;

        _isLevelGenerated = true;
    }

    private GameObject ChoseRandomPickUp() => _pickUps[Random.Range(0, _pickUps.Length)];

    private Vector2 ChoseRandomLocation() => new Vector2((int)Random.Range(_minPosition.x, _maxPosition.x),
        (int)Random.Range(_minPosition.y, _maxPosition.y));

    private float ChoseRandomTimeBetweenSpawns() => Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);

    private bool IsLocationOccupied(Vector2 position) => Physics2D.Raycast(position + _raycastOffset, Vector2.right, _distanceToCheck,
        _layersToIgnore);
}
