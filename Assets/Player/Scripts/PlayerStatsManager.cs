using System;

public class PlayerStatsManager
{
    private int _playerId;
    public int CurrentHealth { get; private set; }

    public bool IsSpawning;
    public bool IsPermamentDead { get; private set; }

    public Action<int> PermamentDeathEvent;
    public Action<int> DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData, int playerId)
    {
        _playerId = playerId;

        CurrentHealth = playerStatsData.health;
        IsSpawning = true;
    }

    public void TakeDamage()
    {
        CurrentHealth--;
        if (CurrentHealth <= 0)
        {
            IsPermamentDead = true;
            PermamentDeathEvent?.Invoke(_playerId);
        }
        else
        {
            DeathEvent?.Invoke(_playerId);
        }
    }
}
