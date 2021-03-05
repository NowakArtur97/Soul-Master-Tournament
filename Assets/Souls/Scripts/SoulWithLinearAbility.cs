using UnityEngine;

public abstract class SoulWithLinearAbility : Soul
{
    protected override Vector2 GetSoulPosition(int range) => (Vector2)transform.position + range * AbilityDirection * transform.right;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, transform.right.x == 1 ? 0 : 180, 0);
}
