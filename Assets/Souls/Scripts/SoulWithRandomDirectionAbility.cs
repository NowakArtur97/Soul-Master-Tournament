using UnityEngine;

public abstract class SoulWithRandomDirectionAbility : Soul
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void UseAbility()
    {
        SoulAbility ability = Instantiate(SoulAbility, GetSoulPosition(6), GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool("start", true);
    }

    protected override Vector2 GetSoulPosition(int range) => new Vector2(Random.Range(1, range), Random.Range(1, range));

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);
}
