using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerPrefabs;
    [SerializeField]
    private int _numberOfPlayers = 1;
    [SerializeField]
    public Vector2[] PlayersPositions;
    [SerializeField]
    private Vector2 _playersPositionOffset = new Vector2(0.6f, 1.2f);
    [SerializeField]
    private string[] _playerColors = { "blue", "green", "orange", "pink" };

    private List<Player> _players;
    private EnvironmentHazardGenerator _environmentHazardGenerator;

    private void Start()
    {
        _environmentHazardGenerator = FindObjectOfType<EnvironmentHazardGenerator>();
        _environmentHazardGenerator.LevelGeneratedEvent += OnLevelGenerated;

        _players = new List<Player>();
    }

    private void OnLevelGenerated()
    {
        _environmentHazardGenerator.LevelGeneratedEvent -= OnLevelGenerated;

        for (int i = 0; i < _numberOfPlayers; i++)
        {
            SpawnPlayer(i);
        }
    }

    private void SpawnPlayer(int id)
    {
        GameObject playerGO = Instantiate(_playerPrefabs[id], PlayersPositions[id] + _playersPositionOffset, Quaternion.identity);
        playerGO.transform.parent = gameObject.transform;
        Player player = playerGO.GetComponent<Player>();

        player.CreateStatsManager(id);
        player.PlayerStatsManager.DeathEvent += OnPlayerDeath;
        player.PlayerStatsManager.PermamentDeathEvent += OnPermamentDeath;
        player.SetColorForAnimation(_playerColors[id]);

        _players.Add(player);
    }

    private void OnPlayerDeath(int id) => _players[id].transform.position = PlayersPositions[id];

    private void OnPermamentDeath(int id)
    {
        _players[id].PlayerStatsManager.DeathEvent -= OnPlayerDeath;
        _players[id].PlayerStatsManager.PermamentDeathEvent -= OnPlayerDeath;
    }
}
