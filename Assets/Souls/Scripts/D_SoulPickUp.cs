using UnityEngine;

[CreateAssetMenu(fileName = "_SoulPickUpData", menuName = "Data/Soul Pick Up")]
public class D_SoulPickUp : ScriptableObject
{
    public GameObject soul;
    public Vector2 positionOffset = new Vector2(0.5f, 0.55f);
    public int numberOfUses = 1;
}
