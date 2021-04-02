using UnityEngine;

[CreateAssetMenu(fileName = "_SoulAbilityStatsData", menuName = "Data/Souls Abilities")]
public class D_SoulAbilityStats : ScriptableObject
{
    public int abilityDexterity = 3;
    public float activeTime = 10f;
    public Vector2 positionOffset = new Vector2(0, 0);
}
