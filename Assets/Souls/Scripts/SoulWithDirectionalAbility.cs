using UnityEngine;

public abstract class SoulWithDirectionalAbility : Soul
{
    protected override Vector2 GetSoulPosition() => (Vector2)transform.position + AbilityRange * AbilityDirection;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);
}
