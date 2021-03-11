using UnityEngine;

[CreateAssetMenu(fileName = "_StatsData", menuName = "Data/Souls Abilities/Stats")]
public class D_SoulAbilityStats : ScriptableObject
{
    public int abilityDexterity = 3;
    public float activeTime = 10f;
    public Vector2 positionOffset = new Vector2(0, 0);
}
