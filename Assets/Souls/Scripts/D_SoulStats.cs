using UnityEngine;

[CreateAssetMenu(fileName = "_SoulStatsData", menuName = "Data/Soul")]
public class D_SoulStats : ScriptableObject
{
    public int abilityRange = 3;
    public float abilityCooldown = 3;
    public float maxAbilityDuration = 5;
    public bool isAbilityTriggeredAfterTime = true;
    public Vector2[] directions;
    public Vector2 startPositionOffset = new Vector2(0.5f, 0.5f);
    public Vector2 abilityPositionOffset = new Vector2(0.5f, 0.5f);
    public LayerMask[] notAfectedLayerMasks;
    public LayerMask[] afectedLayerMasks;
    public bool canPlayerSummonAfterDestroy = true;
}
