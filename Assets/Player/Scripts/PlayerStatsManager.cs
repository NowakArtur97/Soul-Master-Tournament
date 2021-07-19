using System;

public class PlayerStatsManager
{
    private int _playerId;
    private float _maxHealth;
    private float _currentHealth;

    public bool IsPermamentDead { get; private set; }

    public Action<int> PermamentDeathEvent;
    public Action<int> DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData, int playerId)
    {
        _playerId = playerId;

        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        _currentHealth--;
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
}
