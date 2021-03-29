using System;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private int _playerId;
    private float _maxHealth;
    private float _currentHealth;

    public bool IsDead { get; private set; }

    public Action<int> DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData, int playerId)
    {
        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = _maxHealth;

        _playerId = playerId;
    }

    public void TakeDamage()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            IsDead = true;
            DeathEvent?.Invoke(_playerId);
        }
    }
}
