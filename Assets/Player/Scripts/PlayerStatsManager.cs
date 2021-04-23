using System;
using UnityEngine;

public class PlayerStatsManager
{
    private int _playerId;
    private float _maxHealth;
    private float _currentHealth;

    // TODO: Move to statuses manager
    public bool CanMove { get; private set; }
    public float ImmobilityTime { get; private set; }
    public float ImmobilityStartTime { get; private set; }
    public bool IsPermamentDead { get; private set; }

    public Action<int> PermamentDeathEvent;
    public Action<int> DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData, int playerId)
    {
        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = _maxHealth;

        _playerId = playerId;
        CanMove = true;
    }

    public void TakeDamage()
    {
        _currentHealth--;

        // TODO: Player: Check if has shield
        if (_currentHealth <= 0)
        {
            PermamentDeathEvent?.Invoke(_playerId);
            IsPermamentDead = true;
        }
        else
        {
            DeathEvent?.Invoke(_playerId);
        }
    }

    public void Immobilize(float immobilityTime)
    {
        CanMove = false;
        ImmobilityTime = immobilityTime;
        ImmobilityStartTime = Time.time;
    }

    public void UnlockMovement() => CanMove = true;
}
