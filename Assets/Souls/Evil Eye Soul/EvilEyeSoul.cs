public class EvilEyeSoul : SoulWithLinearAbility
{
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME = "middle";
    private const string ABILITY_END_POS_ANIMATION_BOOL_NAME = "end";

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
        else if (IsUsingAbility)
        {
            MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
        else if (IsSummoned && !HasAppeared)
        {
            FinishSummoningSoul(IDLE_ANIMATION_BOOL_NAME);
        }
    }

    protected override string GetAnimationBoolName() =>
        AbilityRange == AbilityMaxRange || CheckIfTouching(AbilityRange + 1, AbilityDirection, SoulStats.notAfectedLayerMasks)
        ? ABILITY_END_POS_ANIMATION_BOOL_NAME : ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME;
}
