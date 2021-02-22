using UnityEngine;

public class FireSoul : Soul
{
    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            Destroy(gameObject);
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
        }
        else if (IsUsingAbility)
        {
            IsUsingAbility = false;
            MyAnimator.SetBool("ability", true);
        }
    }

    protected override void UserAbility(Vector2 abilityDirection)
    {
        int abilityRange = SoulStats.abilityRange;

        Vector2 abilityPosition;

        for (int range = 1; range <= abilityRange; range++)
        {
            abilityPosition = (Vector2)transform.position + range * abilityDirection;

            if (CheckIfTouchingWall(range, abilityDirection))
            {
                return;
            }

            SoulAbility ability = Instantiate(SoulAbility, abilityPosition, Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex));

            string animationBoolName = range != abilityRange ? "middle" : "end";

            ability.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);
        }
    }
}
