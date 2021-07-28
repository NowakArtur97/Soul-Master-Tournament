using System;

public class PlayerStatsManager
{
    private int _playerId;
    private int _maxHealth;
    public int CurrentHealth { get; private set; }

    public bool IsPermamentDead { get; private set; }

    public Action<int> PermamentDeathEvent;
    public Action<int> DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData, int playerId)
    {
        _playerId = playerId;

        _maxHealth = playerStatsData.maxHealth;
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth--;
        if (CurrentHealth <= 0)
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
