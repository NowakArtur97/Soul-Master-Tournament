public class FireSoul : SoulWithDirectionalAbility
{
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME = "middle";
    private const string ABILITY_END_POS_ANIMATION_BOOL_NAME = "end";

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
        }
        else if (IsUsingAbility)
        {
            MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
        else if (IsSummoned && !WasSummoned)
        {
            SummonSoul();
        }
    }

    protected override void SummonSoul()
    {
        base.SummonSoul();
        MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);
    }

    protected override string GetAnimationBoolName() => AbilityRange != AbilityMaxRange
        ? ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME : ABILITY_END_POS_ANIMATION_BOOL_NAME;
}
