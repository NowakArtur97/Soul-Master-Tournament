public class IceSoul : SoulWithLinearAbility
{
    private const string START_ABILITY_ANIMATION_BOOL_NAME = "create";

    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, false);
            UnsummonSoul();
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
        }
        else if (IsSummoned && !HasAppeared)
        {
            FinishSummoningSoul(ABILITY_ANIMATION_BOOL_NAME);
        }
    }

    protected override string GetAnimationBoolName() => START_ABILITY_ANIMATION_BOOL_NAME;
}
