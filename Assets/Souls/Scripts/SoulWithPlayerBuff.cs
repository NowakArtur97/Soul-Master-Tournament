using UnityEngine;

public abstract class SoulWithPlayerBuff : Soul
{
    protected override Vector2 GetSoulPosition(int range) => (Vector2)transform.position + range * AbilityDirection;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);
}
