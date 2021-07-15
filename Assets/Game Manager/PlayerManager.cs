using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerPrefabs;
    [SerializeField]
    public Vector2[] PlayersPositions;
    [SerializeField]
    private Vector2 _playersPositionOffset = new Vector2(0.6f, 1.2f);
    [SerializeField]
    private string[] _playerColors = { "blue", "green", "orange", "pink" };

    private List<int> _playersIndexes;
    private GameObject[] _playersToSpawn;
    private List<Player> _players;
    private EnvironmentHazardGenerator _environmentHazardGenerator;
    private CharacterSelection _characterSelection;

    private void Start()
    {
        _environmentHazardGenerator = FindObjectOfType<EnvironmentHazardGenerator>();

        _environmentHazardGenerator.LevelGeneratedEvent += OnLevelGenerated;

        _characterSelection = CharacterSelection.Instance;
        _playersIndexes = _characterSelection.CharacterIndexes;

        _players = new List<Player>();
    }

    private void OnLevelGenerated()
    {
        _environmentHazardGenerator.LevelGeneratedEvent -= OnLevelGenerated;

        _playersToSpawn = _playersIndexes.Select(index => _playerPrefabs[index]).ToArray();

        for (int i = 0; i < _playersToSpawn.Count(); i++)
        {
            SpawnPlayer(i);
        }
    }

    private void SpawnPlayer(int id)
    {
        GameObject playerGO = Instantiate(_playersToSpawn[id], PlayersPositions[id] + _playersPositionOffset, Quaternion.identity);
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
