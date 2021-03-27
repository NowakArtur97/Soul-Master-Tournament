using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int _numberOfPlayers = 1;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    public Vector2[] PlayersPositions;

    private void Start()
    {
        FindObjectOfType<TileMapGenerator>().LevelGeneratedEvent += OnLevelGenerated;
    }

    private void OnLevelGenerated()
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            Instantiate(_playerPrefab, PlayersPositions[i], Quaternion.identity);
        }

        FindObjectOfType<TileMapGenerator>().LevelGeneratedEvent -= OnLevelGenerated;
    }
}
