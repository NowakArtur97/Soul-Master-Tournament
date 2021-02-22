using UnityEngine;

[CreateAssetMenu(fileName = "_SoulStatsData", menuName = "Data/Soul/Stats")]
public class D_SoulStats : ScriptableObject
{
    public int abilityRange = 3;
    public float abilityCooldown = 3;
    public Vector2[] directions;
    public Vector2 startPositionOffset = new Vector2(0, 0);
    public Vector2 abilityPositionOffset = new Vector2(0, 0);
    public LayerMask[] notAfectedLayerMasks;
    public LayerMask[] afectedLayerMasks;
}
