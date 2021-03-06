public class PoisonousSoul : SoulWithRandomDirectionAbility
{
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string START_USING_ABILITY_ANIMATION_BOOL_NAME = "ability";
    private const string FINISH_USING_ABILITY_ANIMATION_BOOL_NAME = "finish";

    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            MyAnimator.SetBool(FINISH_USING_ABILITY_ANIMATION_BOOL_NAME, false);
            UnsummonSoul();
        }
        else if (HasMaxAbilityTimeFinished)
        {
            MyAnimator.SetBool(START_USING_ABILITY_ANIMATION_BOOL_NAME, false);
            MyAnimator.SetBool(FINISH_USING_ABILITY_ANIMATION_BOOL_NAME, true);
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
        }
        else if (IsUsingAbility)
        {
            MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
            IsUsingAbility = false;
            ShouldStartUsingAbility = true;
        }
        else if (IsSummoned && !HasAppeared)
        {
            FinishSummoningSoul(IDLE_ANIMATION_BOOL_NAME);
        }
    }

    public override void StartUsingAbilityTrigger() => IsUsingAbility = true;

    protected override string GetAnimationBoolName() => START_USING_ABILITY_ANIMATION_BOOL_NAME;
}
