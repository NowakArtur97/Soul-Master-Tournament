public class WaterSoul : SoulWithPlayerBuff
{
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string START_ABILITY_ANIMATION_BOOL_NAME = "active";

    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            UnsummonSoul();
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
            HasUsedAbility = true;
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, false);
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

    protected override string GetAnimationBoolName() => START_ABILITY_ANIMATION_BOOL_NAME;
}
