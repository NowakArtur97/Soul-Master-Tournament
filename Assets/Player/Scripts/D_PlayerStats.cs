using UnityEngine;

[CreateAssetMenu(fileName = "_PlayerStatsData", menuName = "Data/Player Stats")]
public class D_PlayerStats : ScriptableObject
{
    public int health = 3;
    public int maxHealth = 5;
    public int movementSpeed = 10;
    public int startingNumberOfSouls = 1;
}
