using UnityEngine;

[CreateAssetMenu(fileName = "_ProjectileData", menuName = "Data/Projectile")]
public class D_ProjectileStats : ScriptableObject
{
    public float speed = 7f;
    public int damage = 1;
    public float damageRadius = 0.3f;
    public LayerMask[] whatIsDamagable;
    public LayerMask[] whatIsGround;
}