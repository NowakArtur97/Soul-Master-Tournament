using UnityEngine;

public abstract class SoulWithPlayerBuff : Soul
{
    private Vector2 _playerPosition;

    protected override void UseAbility()
    {
        SoulAbility ability = Instantiate(SoulAbility, GetSoulPosition(), GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(), true);
    }

    protected override Vector2 GetSoulPosition() => FindObjectOfType<Player>().transform.position;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);
}
