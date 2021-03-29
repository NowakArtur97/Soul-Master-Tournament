using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int _numberOfPlayers = 1;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    public Vector2[] PlayersPositions;

    private List<Player> _players;

    private void Start()
    {
        FindObjectOfType<TileMapGenerator>().LevelGeneratedEvent += OnLevelGenerated;

        _players = new List<Player>();
    }

    private void OnLevelGenerated()
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            SpawnPlayer(i);
        }

        FindObjectOfType<TileMapGenerator>().LevelGeneratedEvent -= OnLevelGenerated;
    }

    private void SpawnPlayer(int id)
    {
        GameObject playerGO = Instantiate(_playerPrefab, PlayersPositions[id], Quaternion.identity);
        Player player = playerGO.GetComponent<Player>();
        player.ID = id;
        player.PlayerStatsManager.DeathEvent += OnPlayerDeath;
        _players.Add(player);
    }

    private void OnPlayerDeath(int id)
    {
        _players[id].PlayerStatsManager.DeathEvent -= OnPlayerDeath;
        SpawnPlayer(id);
    }
}
