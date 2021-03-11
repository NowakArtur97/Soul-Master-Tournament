using UnityEngine;

public abstract class SoulWithRandomDirectionAbility : Soul
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void StartUsingAbility()
    {
        MyAnimator.SetBool("ability", true);

        transform.position = transform.position - (Vector3)SoulStats.startPositionOffset + (Vector3)SoulStats.abilityPositionOffset;

        UseAbility();
    }

    protected override void UseAbility()
    {
        var pos = GetSoulPosition(6);
        Debug.Log(pos);
        SoulAbility ability = Instantiate(SoulAbility, pos, GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool("start", true);

        HasUsedAbility = true;
    }

    protected override Vector2 GetSoulPosition(int range) => new Vector2(Random.Range(1, range), Random.Range(1, range));

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);
}
