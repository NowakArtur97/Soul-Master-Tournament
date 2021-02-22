using UnityEngine;

public class EvilEyeSoul : Soul
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

    protected override void UserAbility(Vector2 explosionDirection)
    {
        int explosionRange = SoulStats.abilityRange;

        Vector2 explosionPosition;

        for (int range = 1; range <= explosionRange; range++)
        {
            explosionPosition = (Vector2)transform.position + range * explosionDirection;

            if (CheckIfTouchingWall(range, explosionDirection))
            {
                return;
            }

            SoulAbility explosion = Instantiate(SoulAbility, explosionPosition, Quaternion.identity);

            string animationBoolName = range != explosionRange ? "middle" : "end";

            explosion.GetComponentInChildren<Animator>().SetBool(animationBoolName, true);
        }
    }
}
