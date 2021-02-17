using UnityEngine;

[CreateAssetMenu(fileName = "_SoulStatsData", menuName = "Data/Soul/Stats")]
public class D_SoulStats : ScriptableObject
{
    public int explosionRange = 3;
    public float timeToExplode = 3;
    public Vector2[] directions;
    public Vector2 positionOffset = new Vector2(0, 0);
}
