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
    private float _minTimeBetweenSpawns = 1;
    [SerializeField]
    private float _maxTimeBetweenSpawns = 10;

    private Coroutine _spawnCoroutine;
    private bool _isSpawning;
    private bool _isLevelGenerated;

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

        yield return new WaitForSeconds(ChoseRandomTimmeBetwenSpawns());

        GameObject pickUp = Instantiate(ChoseRandomPickUp(), ChoseRandomLocation(), Quaternion.identity);
        pickUp.transform.parent = gameObject.transform;

        _isSpawning = false;
    }

    private void OnLevelGenerated()
    {
        _environmentHazardGenerator.LevelGeneratedEvent -= OnLevelGenerated;

        _isLevelGenerated = true;
    }

    private GameObject ChoseRandomPickUp() => _pickUps[Random.Range(0, _pickUps.Length)];

    private Vector2 ChoseRandomLocation() => new Vector2((int)Random.Range(_minPosition.x, _maxPosition.x), (int)Random.Range(_minPosition.y, _maxPosition.y));

    private float ChoseRandomTimmeBetwenSpawns() => Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
}
