using UnityEngine;

public abstract class SoulWithRandomDirectionAbility : Soul
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void UseAbility()
    {
        SoulAbility ability;

        int range = GetRandomRange(AbilityRange);

        if (CheckIfTouchingWall(range, AbilityDirection, SoulStats.notAfectedLayerMasks))
        {
            return;
        }

        var pos = GetSoulPosition(AbilityRange);

        Debug.Log(pos);
        ability = Instantiate(SoulAbility, pos, GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(range), true);
    }

    protected override Vector2 GetSoulPosition(int range) => new Vector2(Random.Range(1, range), Random.Range(1, range));

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    private int GetRandomRange(int maxRange) => Random.Range(1, maxRange);
}
