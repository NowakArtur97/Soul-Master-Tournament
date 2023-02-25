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
            if (Player.PlayerStatsManager.IsSpawning || Player == null)
            {
                ResetAllAnimatorBoolVariables();
                HasUsedAbility = true;
            }
            else
            {
                ShouldStartUsingAbility = false;
                StartUsingAbility();
            }
        }
        else if (IsUsingAbility && MyAnimator.GetBool(ABILITY_ANIMATION_BOOL_NAME) != true)
        {
            if (Player.PlayerStatsManager.IsSpawning || Player == null)
            {
                ResetAllAnimatorBoolVariables();
                HasUsedAbility = true;
            }
            else
            {
                MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
                MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
            }
        }
        else if (IsSummoned && !HasAppeared)
        {
            FinishSummoningSoul(IDLE_ANIMATION_BOOL_NAME);
        }
    }

    protected override string GetAnimationBoolName() =>
        AbilityRange == AbilityMaxRange
        || CheckIfTouching(AbilityRange + 1, AbilityDirection * transform.right, SoulStats.notAfectedLayerMasks)
        ? ABILITY_END_POS_ANIMATION_BOOL_NAME : ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME;
}
