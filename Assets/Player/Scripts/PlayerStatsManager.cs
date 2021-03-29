using System;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private int _playerId;
    private float _maxHealth;
    private float _currentHealth;

    public bool IsDead { get; private set; }

    public Action<int> PermamentDeathEvent;
    public Action<int> DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData, int playerId)
    {
        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = _maxHealth;

        _playerId = playerId;
    }

    public void TakeDamage(AttackDetails attackDetails)
    {
        _currentHealth -= attackDetails.damageAmount;

        // TODO: Player: Check if has shield
        if (_currentHealth <= 0)
        {
            // TODO: Player: Stop Spawning Player
            PermamentDeathEvent?.Invoke(_playerId);
        }
        else
        {
            DeathEvent?.Invoke(_playerId);
        }

        IsDead = true;
    }
}
