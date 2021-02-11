using UnityEngine;

[CreateAssetMenu(fileName = "_PlayerStatsData", menuName = "Data/Player/Stats")]
public class D_PlayerStats : ScriptableObject
{
    public int health = 3;
    public int maxHealth = 5;
    public int speed = 10;
    public int maxSpeed = 14;
    public int numberOfBombs = 1;
    public int maxNumberOfBombs = 8;
}
